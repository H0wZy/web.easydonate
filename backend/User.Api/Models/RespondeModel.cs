namespace User.Api.Models;

public class RespondeModel<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}