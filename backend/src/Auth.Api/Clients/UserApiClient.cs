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
        Console.WriteLine($"DEBUG: Iniciando consumo do endpoint GetUserByEmail da User.Api");

        var url = $"{_userApiBaseUrl}/GetUserByEmail/{email}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"DEBUG: Status: {response.StatusCode}\nDEBUG: Response: {content}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode} - {content}");
        }

        var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

        if (responseData?.Data is null)
        {
            throw new Exception($"Invalid response format: {content}");
        }

        Console.WriteLine("DEBUG: Finalizando com sucesso o consumo do endpoint GetUserByEmailAsync da User.Api");
        return responseData.Data;
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var url = $"{_userApiBaseUrl}/GetUserByUsername/{username}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

        return responseData?.Success == true ? responseData.Data : null;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var url = $"{_userApiBaseUrl}/GetUserById/{id}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var responseData = JsonConvert.DeserializeObject<ResponseModel<UserDto>>(content);

        return responseData?.Success == true ? responseData.Data : null;
    }
}