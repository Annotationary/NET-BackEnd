
using Jso.Annotationary.API.Middleware;
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
            
            // =============================
            // Services configuration
            // =============================
            builder.Services.AddControllers();
            
            // DbContext
            builder.Services.AddDbContext<AnnotationaryDbContext>(options => 
                options.UseMySql(
                    builder.Configuration.GetConnectionString("Default"),
                    ServerVersion.AutoDetect(
                        builder.Configuration.GetConnectionString("Default")
                        )
                    )
                );
            
            // Dependency Injection
            builder.Services.AddScoped<AnnotationaryDbContext>();

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

            // Middleware pipeline
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
