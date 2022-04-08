using Elfland.IdentityServer.Data;
using Elfland.IdentityServer.Data.Initializers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Serilog
IConfiguration _configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
        optional: true,
        reloadOnChange: true
    )
    .AddEnvironmentVariables()
    .Build();


Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(_configuration)
    .Enrich.FromLogContext()
    .CreateLogger();


try
{
    Log.Information("Starting web host");

    // StartUp
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(
        (hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
    );

    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add DbContexts
    builder.Services.AddDbContext<ApplicationDbContext>(
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

            // Register the entity sets needed by OpenIddict.
            // Note: use the generic overload if you need to replace the default OpenIddict entities.
            options.UseOpenIddict();
        }
    );

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    // Add MediatR
    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    // AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


    var app = builder.Build();

    // Seed data
    try
    {
        await app.Services.InitializeDatabaseAsync();
    }
    catch (System.Exception ex)
    {
        Log.Information(ex, "An error occurred while seeding the database.");
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

#if(https)
    app.UseHttpsRedirection();
#endif

    app.UseAuthentication();

#if (authorization)
    app.UseAuthorization();
#endif

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
