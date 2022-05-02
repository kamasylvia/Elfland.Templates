using Elfland.Lake.Extensions;
using Elfland.WebApi.Data.Initializers;
using Elfland.WebApi.Infrastructure.Extensions.ProgramExtensions;
using Elfland.WebApi.Infrastructure.Filters;

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog
    builder.AddCustomSerilog();

    // Add services to the container.
    builder.Services.AddControllers(
        options =>
        {
            options.Filters.Add<HttpGlobalExceptionFilterAttribute>();
        }
    );

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add database
    builder.AddCustomDatabase();
    // Add MediatR
    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    // Add AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Add custom services
    builder.Services.AddApplicationServices();

    var app = builder.Build();

    // Seed data
    try
    {
        if (
            args.Length == 1
            && (args[0].ToLower().Contains("seed") || args[0].ToLower().Contains("init"))
        )
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
