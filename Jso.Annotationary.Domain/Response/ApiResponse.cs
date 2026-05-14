namespace Jso.Annotationary.Domain.Response;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>?  Errors { get; set; }
    
    // Helper class when return success
    public static ApiResponse<T> Success(T data, string message = "Success", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            StatusCode = statusCode,
            Errors = null
        };
    }
    
    // Helper class when return fail
    public static ApiResponse<T> Failure(List<string> errors, string message, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            Message = message,
            Data = default,
            StatusCode = statusCode,
            Errors = errors
        };
    }
    
}