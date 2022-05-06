using System.Text.Json.Serialization;
using Dapr.Extensions.Configuration;
using Elfland.Dapr.Data.Initializers;
using Elfland.Dapr.Infrastructure.Extensions.ProgramExtensions;
using Elfland.Dapr.Infrastructure.Filters;
using Elfland.Lake.Extensions;
using Elfland.Ocean.Extensions;
#if (grpcServer || grpcClientServer)
using Elfland.Dapr.Services;
#endif

try
{
    Log.Information("Starting web host");

    // StartUp
    var builder = WebApplication.CreateBuilder(args);

    // Merge Dapr secret store into Configuration
    builder.Configuration.AddDaprSecretStore("secretStore", new DaprClientBuilder().Build());

    // Add Serilog
    builder.AddCustomSerilog();

    // Add services to the container.
    builder.Services
        .AddControllers(
            options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilterAttribute>();
            }
        )
        .AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
            }
        )
        .AddDapr();

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
    // Add Actors
    builder.Services.AddDaprActors();

#if (grpcServer || grpcClientServer)
    // Add gRPC
    builder.Services.AddGrpc();
#endif

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

#if (https)
    app.UseHttpsRedirection();

#endif
#if (auth)
    app.UseAuthorization();

#endif
    app.UseCloudEvents();

    app.MapControllers();

    app.MapActorsHandlers();

    app.MapSubscribeHandler();

#if (grpcServer || grpcClientServer)
    app.MapGrpcService<GrpcService>();

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
