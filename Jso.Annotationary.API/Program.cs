
using Jso.Annotationary.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Jso.Annotationary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Annotationary API",
                    Version = "v1",
                    Description = "Clean Architecture API"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Annotationary v1");
                    options.RoutePrefix = string.Empty; // mở thẳng tại root "/"
                });
            }

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVite",
                    policy =>
                    {
                        policy.WithOrigins(
                                "http://localhost:5173",
                                "http://localhost:3000",
                                "http://localhost:5174"
                              )
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
                    });
            });

            // DbConext injection
            var connectionString = builder.Configuration.GetConnectionString("Default");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            builder.Services.AddDbContext<AnnotationaryDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
