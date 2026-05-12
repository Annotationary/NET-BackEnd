using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Infrastructure.Context;
using Jso.Annotationary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jso.Annotationary.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<AnnotationaryDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
