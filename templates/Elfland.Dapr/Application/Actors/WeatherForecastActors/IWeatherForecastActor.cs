using Elfland.Dapr.Application.Queries.WeatherForecastQueries;

namespace Elfland.Dapr.Application.Actors.WeatherForecastActors;

public interface IWeatherForecastActor : IActor
{
    Task<IEnumerable<GetWeatherForecastResponse>> GetWeatherForecastAsync(
        GetWeatherForecastRequest request
    );
}
