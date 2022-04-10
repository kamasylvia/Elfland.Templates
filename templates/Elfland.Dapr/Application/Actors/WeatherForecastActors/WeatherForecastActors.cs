using AutoMapper;
using Dapr.Actors.Runtime;
using Elfland.Dapr.Application.Queries.WeatherForecastQueries;
using Elfland.Dapr.Data.Entities;

namespace Elfland.Dapr.Application.Actors.WeatherForecastActors;

public class WeatherForecastActor : Actor, IWeatherForecastActor
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    private readonly IMapper _mapper;

    public WeatherForecastActor(ActorHost host, IMapper mapper) : base(host)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<GetWeatherForecastResponse>> GetWeatherForecastAsync(
        GetWeatherForecastRequest request
    )
    {
        var weatherForecasts = Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }
            )
            .ToArray();
        var response = _mapper.Map<IEnumerable<GetWeatherForecastResponse>>(weatherForecasts);
        return Task.FromResult(response);
    }
}
