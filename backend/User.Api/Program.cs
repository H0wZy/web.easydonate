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
// builder.Services.AddSwaggerGen();

// Repositórios e Serviços
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper
builder.Services.AddAutoMapper(_ => { }, typeof(UserProfile));

//DateTime Converter


builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsHistoryTable("__EFMigrationsHistory", "user")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

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

    // app.UseSwagger();
    // app.UseSwaggerUI(c =>
    // {
    //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.Api v1");
    //     c.RoutePrefix = string.Empty;
    // });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();