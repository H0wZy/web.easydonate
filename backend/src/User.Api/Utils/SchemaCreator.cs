using Microsoft.EntityFrameworkCore;
using User.Api.Data;

namespace User.Api.Utils;

public static class SchemaCreator
{
    public static async Task CreateUserSchemaAsync(UserDbContext dbContext)
    {
        try
        {
            await dbContext.Database.ExecuteSqlRawAsync("CREATE SCHEMA \"user\";");
            Console.WriteLine("Schema 'user' criado com sucesso.");
        }
        catch (Npgsql.PostgresException ex) when (ex.SqlState == "42P06")
        {
            Console.WriteLine("Schema 'user' já existe, continuando...");
        }
    }
}