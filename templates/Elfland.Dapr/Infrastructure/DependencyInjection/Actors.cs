using System.Text.Json;
using Elfland.Dapr.Application.Actors.WeatherForecastActors;

namespace Elfland.Dapr.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionDependencyInjection
{
    public static void AddAppActors(this IServiceCollection services)
    {
        services.AddActors(
            options =>
            {
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                options.Actors.RegisterActor<WeatherForecastActor>();
            }
        );
    }
}
