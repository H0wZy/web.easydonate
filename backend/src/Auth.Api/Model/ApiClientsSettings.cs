namespace Auth.Api.Model;

public class ApiClientsSettings(UserApiSettings userApi)
{
    public UserApiSettings UserApi { get; init; } = userApi;
}

public class UserApiSettings(string baseUrl)
{
    public string BaseUrl { get; init; } = baseUrl;
    public int Timeout { get; init; }
}