namespace Auth.Api.Model
{
    public class ApiClientsSettings
    {
        public ApiClientsSettings() { } // construtor padrão obrigatório
        public UserApiSettings UserApi { get; set; } = new();
    }

    public class UserApiSettings
    {
        public UserApiSettings() { } // construtor padrão obrigatório
        public string BaseUrl { get; set; } = string.Empty;
        public int Timeout { get; set; } = 30; // valor padrão opcional
    }
}