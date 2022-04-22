using Elfland.Dapr.Data;

namespace Elfland.Dapr.Infrastructure.Extensions.ProgramExtensions;

public static partial class ProgramExtensions
{
    public static void AddCustomDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
#if (postgres)
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
#elif (mssql)
                options.UseSqlServer(Configuration.GetConnectionString("MsSQL"))
#elif (SQLite)
                options.UseSqlite(Configuration.GetConnectionString("SQLite"));
#elif (mysql)
                var connectionString = builder.Configuration.GetConnectionString("MySQL");
                var serverVersion = MySqlServerVersion.AutoDetect(connectionString);
                options.UseMySql(connectionString, serverVersion);
#endif
            }
        );

        services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
