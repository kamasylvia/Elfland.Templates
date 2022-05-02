using Elfland.Dapr.Application.Actors.WeatherForecastActors;

namespace Elfland.Dapr.Application.Queries.WeatherForecastQueries;

public class GetWeatherForecastQueryHandler
    : IRequestHandler<GetWeatherForecastRequest, IEnumerable<GetWeatherForecastResponse>>
{
    private readonly IActorProxyFactory _actorProxyFactory;

    public GetWeatherForecastQueryHandler(IActorProxyFactory actorProxyFactory)
    {
        _actorProxyFactory =
            actorProxyFactory ?? throw new ArgumentNullException(nameof(actorProxyFactory));
    }

    public Task<IEnumerable<GetWeatherForecastResponse>> Handle(
        GetWeatherForecastRequest request,
        CancellationToken cancellationToken
    )
    {
        return _actorProxyFactory
            .CreateActorProxy<IWeatherForecastActor>(
                ActorId.CreateRandom(),
                nameof(WeatherForecastActor)
            )
            .GetWeatherForecastAsync(request);
    }
}
