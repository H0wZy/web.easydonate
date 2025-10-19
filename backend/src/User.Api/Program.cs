using System.ComponentModel;
using User.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using User.Api.Mappings;
using User.Api.Repositories.UserRepository;
using User.Api.Services.UserService;
using User.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Repositórios e Serviços
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper
builder.Services.AddAutoMapper(_ => { }, typeof(UserProfile));

//DateTime Converter
builder.Services.AddSingleton<JsonDateTimeConverter>();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsHistoryTable("__EFMigrationsHistory", "user")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// Aplica migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    try
    {
        db.Database.ExecuteSqlRaw("CREATE SCHEMA IF NOT EXISTS \"user\";");
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao migrar banco: " + ex.Message);
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "User.Api";
        options.Theme = ScalarTheme.Default;
        options.DefaultHttpClient =
            new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();