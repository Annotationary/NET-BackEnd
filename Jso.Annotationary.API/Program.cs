
using Jso.Annotationary.API.Middleware;
using Jso.Annotationary.Application.Interfaces;
using Jso.Annotationary.Application.Services;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Infrastructure.Context;
using Jso.Annotationary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Jso.Annotationary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // =============================
            // Services configuration
            // =============================
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });
            
            // DbContext
            var connectionString = builder.Configuration.GetConnectionString("Dev");
            builder.Services.AddDbContext<AnnotationaryDbContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
            
            // Swagger
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
            
            // Dependency Injection
            builder.Services.AddScoped<AnnotationaryDbContext>();
            
            // Register AutoMapper
            builder.Services.AddAutoMapper(cfg => {}, typeof(UserService).Assembly);
            
            // Register services
            builder.Services.AddScoped<IUserService, UserService>();
            
            // Register repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            
            // =============================
            // Middleware pipeline
            // =============================
            var app = builder.Build();
            
            app.UseMiddleware<GlobalExceptionMiddleware>();
            
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
            
            // if (!app.Environment.IsDevelopment())
            // {
            //     app.UseHttpsRedirection();
            // }

            app.UseCors("AllowVite");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
