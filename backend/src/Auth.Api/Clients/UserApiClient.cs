using Auth.Api.Dto;
using Auth.Api.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Auth.Api.Clients;

public class UserApiClient(HttpClient httpClient, IOptions<ApiClientsSettings> apiSettings)
    : IUserApiClient
{
    private readonly string _userApiBaseUrl = $"{apiSettings.Value.UserApi.BaseUrl}/api/User";

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        Console.WriteLine("DEBUG: Iniciando consumo do endpoint GetUserByEmail da User.Api");

        var url = $"{_userApiBaseUrl}/GetUserByEmail/{email}";
        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"DEBUG: Status: {response.StatusCode}\nDEBUG: Response: {content}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("DEBUG: Usuário não encontrado ou erro na API");
            return null;
        }

        try
        {
            var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

            if (responseData?.Success != true || responseData.Data == null)
            {
                Console.WriteLine("DEBUG: Response inválida ou sem dados");
                return null;
            }

            Console.WriteLine("DEBUG: Usuário encontrado com sucesso");
            return responseData.Data;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"DEBUG: Erro ao deserializar JSON: {ex.Message}");
            return null;
        }
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var url = $"{_userApiBaseUrl}/GetUserByUsername/{username}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        
        try
        {
            var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

            return responseData?.Success == true ? responseData.Data : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var url = $"{_userApiBaseUrl}/GetUserById/{id}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        
        try
        {
            var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

            return responseData?.Success == true ? responseData.Data : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}