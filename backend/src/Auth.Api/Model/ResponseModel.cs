namespace Auth.Api.Model;

public record ResponseModel<T>(
    bool Success,
    string Message,
    T? Data = default
)
{
    public static ResponseModel<T> Ok(T data, string message = "Operação realizada com sucesso.")
        => new(true, message, data);

    public static ResponseModel<T> Fail(string message)
        => new(false, message, default);
}