node {
    try {
        stage('Checkout') {
            checkout scm
        }

        // Configs
        def config = [
            appName: 'annotationary-be',
            harborURL: 'harbor.homelab',
            harborProject: 'annotationary',

            release: '1.0.0',
            dev: '1.0.0',

            containerPort: '8081',
            testPort: '8081',
            devPort: '8082',
            prodPort: '8084',

            devServer: 'deployer@dev-test01.homelab',
            prodServer: 'deployer@dev-test01.homelab',

            manifestRepo: 'https://github.com/G4-Data-Labeling-Support-System/Infrastructure.git',
            // env: '${env.BRANCH_NAME == 'main' ? 'production' : 'development'}',
            // k8sNamespace: '${env.BRANCH_NAME == 'main' ? 'prod' : 'dev'}'
        ]

        // Env variables
//        def buildPipeline = load "ci/build.groovy"
        def sonarqubePipeline = load "ci/sonarqube.groovy"
//        def trivyFilesystemScan = load "ci/trivy-filesystem-scan.groovy"
        def dockerBuildPipeline = load "ci/docker-build.groovy"

//        def deployProd = load "ci/deploy-prod.groovy"
//        def deployBeta = load "ci/deploy-beta.groovy"
        def deployDev = load "ci/deploy-dev.groovy"

//        def updateManifest = load "ci/update-manifest.groovy"

        // Call functions base on branch
        if (env.BRANCH_NAME == "main") {
            // buildPipeline.call(config)
            // sonarqubePipeline.call(config)
//            trivyFilesystemScan.call()
//            dockerPipeline.call(config)
//            deployProd.call(config)
            // updateManifest.call(config)
        } else if (env.BRANCH_NAME == "develop") {
            dockerBuildPipeline.call(config)
        }

        stage('Send success notification') {
            withCredentials([string(credentialsId: 'discord-webhook-url', variable: 'DISCORD_WEBHOOK_URL')]) {
                sh '''
                    curl -H "Content-Type: application/json" \
                    -X POST \
                    -d "{\\"content\\":\\"✅ Jenkins job ${JOB_NAME} #${BUILD_NUMBER} deploy successfully: ${BUILD_URL}/\\"}" \
                    "$DISCORD_WEBHOOK_URL"
                '''
            }
        }
    }
    catch (err) {
        currentBuild.result = 'FAILURE'

        stage('Send failure notification') {
            withCredentials([string(credentialsId: 'discord-webhook-url', variable: 'DISCORD_WEBHOOK_URL')]) {
                sh '''
                    curl -H "Content-Type: application/json" \
                    -X POST \
                    -d "{\\"content\\":\\"❌ Jenkins job ${JOB_NAME} #${BUILD_NUMBER} failed to deploy: ${BUILD_URL}/\\"}" \
                    "$DISCORD_WEBHOOK_URL"
                '''
            }
        }
        throw err // keep pipeline failed
    } finally {
        // Clean up workspace after run the pipeline
        stage('Cleanup') {
            cleanWs()

            // Docker cleanup
            // sh"""
            //     docker rmi ${DOCKER}
            // """
        }
    }

}