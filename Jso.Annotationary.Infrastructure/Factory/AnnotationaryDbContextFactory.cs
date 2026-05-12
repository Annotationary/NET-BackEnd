using Jso.Annotationary.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jso.Annotationary.Infrastructure.Factory;

public class AnnotationaryDbContextFactory : IDesignTimeDbContextFactory<AnnotationaryDbContext>
{
    public AnnotationaryDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString =
            configuration.GetConnectionString("Default");

        var optionsBuilder =
            new DbContextOptionsBuilder<AnnotationaryDbContext>();

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));

        return new AnnotationaryDbContext(optionsBuilder.Options);
    }
}