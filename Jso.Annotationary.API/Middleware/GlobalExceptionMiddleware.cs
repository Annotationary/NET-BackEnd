using System.Text.Json;
using Jso.Annotationary.Domain.Response;

namespace Jso.Annotationary.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Allow request continue to Controller 
            await _next(context); 
        }
        catch (Exception ex)
        {
            // Sum all Exception output and process it
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is FluentValidation.ValidationException validationException)
        {
            context.Response.StatusCode = 400;
            var response = ApiResponse<object>.Failure(
                errors: validationException.Errors.Select(e => e.ErrorMessage).ToList(),
                message: "Validation Failed",
                statusCode: context.Response.StatusCode
                );
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
        
        context.Response.StatusCode = 500; // Default is server error
        var errorResponse = ApiResponse<object>.Failure(
            errors: new List<string> { exception.Message },
            message: "Server error occurred, please try again later.",
            statusCode: context.Response.StatusCode
        );
        var defaultJson = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(defaultJson);
    }
}