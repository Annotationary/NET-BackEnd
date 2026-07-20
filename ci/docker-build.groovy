def call(config) {
    String image = "${config.appName}"
    def dockerImage
    String version
    def securityLevel

    if (env.BRANCH_NAME == 'main') {
        version = "${config.release}-release.${env.BUILD_NUMBER}b" // 1.1.2-release.78b
    } else if (env.BRANCH_NAME == 'develop') {
        version = "dev-${env.BUILD_NUMBER}b" // dev-78b
    }

    String imageTagged = "${image}:${version}" // harbor.homelab/annotationary/annotationary-be:1.1.2-release.78b

    stage('Docker Build') {
        echo "Building Docker image: ${imageTagged}"
        dockerImage = docker.build("${imageTagged}", "-f Jso.Annotationary.API/Dockerfile .")
    }

    stage('Trivy Docker Image Scan') {
            script {
                securityLevel = env.BRANCH_NAME == 'main' ? 'HIGH,CRITICAL' : 'CRITICAL'

                sh """
                    trivy image --no-progress \
                    --format json \
                    --severity ${securityLevel} \
                    --output trivyimage.json \
                    ${imageTagged} || true

                    trivy image --no-progress \
                    --format table \
                    --severity ${securityLevel} \
                    --output trivyimage.txt \
                    ${imageTagged}

                    cat trivyimage.txt
                """
            }
            archiveArtifacts artifacts: 'trivyimage.txt,trivyimage.json', allowEmptyArchive: true
    }

//     stage('Docker Test') {
//         script {
//             String containerName = "test-${config.appName}-${env.BUILD_NUMBER}"
//
//             sh """
//                 docker run -d --name ${containerName} \
//                 -p ${config.testPort}:${config.testPort} \
//                 -e SPRING_PROFILES_ACTIVE=test \
//                 ${imageTagged}
//
//                 echo "Waiting for Spring Boot health check..."
//
//                 ATTEMPTS=40
//                 SLEEP=3
//
//                 for i in \$(seq 1 \$ATTEMPTS); do
//                     if curl -fs http://localhost:${config.testPort}/actuator/health > /dev/null; then
//                         echo "App is UP"
//                         break
//                     fi
//
//                     echo "Attempt \$i/\$ATTEMPTS"
//                     sleep \$SLEEP
//                 done
//
//                 curl -f http://localhost:${config.testPort}/actuator/health
//
//                 docker stop ${containerName}
//                 docker rm ${containerName}
//             """
//         }
//     }

    stage('Push Docker Image') {
        withDockerRegistry(
            credentialsId: 'docker_harbor_login',
            url: 'https://harbor.homelab'
        ) {
            if (env.BRANCH_NAME == 'main') {
                dockerImage.push()         // version tag
                dockerImage.push('release-latest') // production latest
            } else if (env.BRANCH_NAME == 'develop') {
                dockerImage.push()            // version tag
                dockerImage.push('beta-latest') // beta latest
            } else {
                dockerImage.push('dev-latest') // dev latest
            }
        }
    }
}

return this