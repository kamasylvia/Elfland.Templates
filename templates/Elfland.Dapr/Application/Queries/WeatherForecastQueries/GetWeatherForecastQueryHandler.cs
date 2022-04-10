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
    : IRequestHandler<
#if (grpc && !clientMode)
        GetWeatherForecastGrpcRequest,
#else
        GetWeatherForecastRequest,
#endif
        IEnumerable<GetWeatherForecastResponse>
    >
{
#if (actors)
    private readonly IActorProxyFactory _actorProxyFactory;
#else
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
#if (grpc && !clientMode)
        GetWeatherForecastGrpcRequest grpcRequest,
#else
        GetWeatherForecastRequest request,
#endif
        CancellationToken cancellationToken
    )
    {
#if (grpc && !clientMode)
    var request = grpcRequest.Request;
#endif

#if (actors)
        return _actorProxyFactory
            .CreateActorProxy<IWeatherForecastActor>(
                ActorId.CreateRandom(),
                nameof(WeatherForecastActor)
            )
            .GetWeatherForecastAsync(request);
#else
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
#endif
    }
}
