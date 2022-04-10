using AutoMapper;
#if (actors)
using Dapr.Actors;
using Dapr.Actors.Client;
using Elfland.Dapr.Application.Actors.WeatherForecastActors;
#endif
using Elfland.Dapr.Data.Entities;
using MediatR;

namespace Elfland.Dapr.Application.Queries.WeatherForecastQueries;

public class GetWeatherForecastQueryHandler
    : IRequestHandler<GetWeatherForecastRequest, IEnumerable<GetWeatherForecastResponse>>
{
#if (!actor)
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
#else
    private readonly IActorProxyFactory _actorProxyFactory;
#endif
    private readonly IMapper _mapper;

    public GetWeatherForecastQueryHandler(
#if (actors)
        IActorProxyFactory actorProxyFactory,
#endif
        IMapper mapper
    )
    {
#if (actors)
        _actorProxyFactory =
            actorProxyFactory ?? throw new ArgumentNullException(nameof(actorProxyFactory));
#endif
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<GetWeatherForecastResponse>> Handle(
        GetWeatherForecastRequest request,
        CancellationToken cancellationToken
    )
    {
#if (!actors)
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
#else
        return _actorProxyFactory
            .CreateActorProxy<IWeatherForecastActor>(
                ActorId.CreateRandom(),
                nameof(WeatherForecastActor)
            )
            .GetWeatherForecastAsync(request);
#endif
    }
}
