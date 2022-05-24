using Elfland.Dapr.Data;

namespace Elfland.Dapr.Extensions.ProgramExtensions;

public static partial class ProgramExtensions
{
    public static void AddCustomDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(
            options =>
            {
#if (postgres)
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
#elif (mssql)
                options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL"))
#elif (SQLite)
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
#elif (mysql)
                var connectionString = builder.Configuration.GetConnectionString("MySQL");
                var serverVersion = MySqlServerVersion.AutoDetect(connectionString);
                options.UseMySql(connectionString, serverVersion);
#endif
            }
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
