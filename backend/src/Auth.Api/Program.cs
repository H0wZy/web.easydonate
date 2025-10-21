using Auth.Api.Clients;
using Auth.Api.Model;
using Auth.Api.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<ApiClientsSettings>(builder.Configuration.GetSection("ApiClients"));
builder.Services.AddHttpClient<UserApiClient>(client =>
{
    var apiSettings = builder.Configuration.GetSection("ApiClients:UserApi").Get<UserApiSettings>();
    if (apiSettings != null) client.Timeout = TimeSpan.FromSeconds(apiSettings.Timeout);
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserApiClient, UserApiClient>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();