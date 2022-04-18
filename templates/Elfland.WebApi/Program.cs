using Elfland.Lake.Extensions;
using Elfland.WebApi.Data;
using Elfland.WebApi.Data.Initializers;
using Elfland.WebApi.Infrastructure.Filters;
using MediatR;
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

    var builder = WebApplication.CreateBuilder(args);

    // StartUp
    builder.Host.UseSerilog(
        (hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
    );

    // Add services to the container.
    builder.Services.AddControllers(
        options => {
            options.Filters.Add<HttpGlobalExceptionFilterAttribute>();
        }
    );

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


    // Add MediatR
    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    // AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Add custom dependencies
    builder.Services.AddDependencies();


    var app = builder.Build();

    // Seed data
    try
    {
        if (args.Length == 1 && (args[0].ToLower().Contains("seed")
            || args[0].ToLower().Contains("init")))
        {
            await app.Services.InitializeDatabaseAsync();
        }
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

#if (auth)
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
