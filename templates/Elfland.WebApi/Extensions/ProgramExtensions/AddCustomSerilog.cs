namespace Elfland.WebApi.Extensions.ProgramExtensions;

public static partial class ProgramExtensions
{
    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom
            .Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog(
            (hostingContext, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
        );
    }
}
