namespace Auth.Api.Models.Response;

public record ApiResponse<T>(
    bool Success,
    string Message,
    T? Data = default,
    IReadOnlyList<string>? Errors = null
)
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    // Factory Methods
    public static ApiResponse<T> CreateSuccess(T data, string message = "Operação realizada com sucesso") =>
        new(true, message, data);

    public static ApiResponse<T> CreateError(string message, IReadOnlyList<string>? errors = null) =>
        new(false, message, default, errors);

    public static ApiResponse<T> CreateError(string message, string error) =>
        new(false, message, default, [error]);

    public static ApiResponse<T> CreateError(string message, params string[] errors) =>
        new(false, message, default, errors);
}