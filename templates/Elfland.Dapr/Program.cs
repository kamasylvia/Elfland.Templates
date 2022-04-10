using Elfland.Dapr.Data;
using Elfland.Dapr.Data.Initializers;
#if (!clientMode)
using Elfland.Dapr.Services;
#endif
#if (actors)
using Elfland.Dapr.Infrastructure.DependencyInjection;
#endif
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
#if (!serverMode)
    builder.Services.AddControllers().AddDapr();
#else
    builder.Services.AddControllers();
#endif

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
        }
    );

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    // Add MediatR
    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    // AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#if (actors)
    // Add Actors
    builder.Services.AddAppActors();
#endif

#if (grpc && !clientMode)
    builder.Services.AddGrpc();
#endif

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

#if (https)
    app.UseHttpsRedirection();
#endif

#if (auth)
    app.UseAuthorization();
#endif

    app.MapControllers();

#if (grpc && !clientMode)
    app.MapGrpcService<GrpcService>();
#endif

#if (actors)
    app.MapActorsHandlers();
#endif

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
