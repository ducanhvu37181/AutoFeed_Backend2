using System;

namespace AutoFeed_Backend_Services.DTOs.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ApiResponse(bool success, string message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> Ok(T? data, string? message = null) => new ApiResponse<T>(true, message ?? "Success", data);
    public static ApiResponse<T> Fail(string message) => new ApiResponse<T>(false, message, default);
}
