using Dapr.Actors;
using Dapr.Actors.Client;
using Elfland.Dapr.Application.Actors.WeatherForecastActors;
using MediatR;

namespace Elfland.Dapr.Application.Queries.WeatherForecastQueries;

public class GetWeatherForecastQueryHandler
    : IRequestHandler<
#if (grpc)
        GetWeatherForecastGrpcRequest,
#else
        GetWeatherForecastRequest,
#endif
        IEnumerable<GetWeatherForecastResponse>
    >
{
    private readonly IActorProxyFactory _actorProxyFactory;

    public GetWeatherForecastQueryHandler(
        IActorProxyFactory actorProxyFactory
    )
    {
        _actorProxyFactory =
            actorProxyFactory ?? throw new ArgumentNullException(nameof(actorProxyFactory));
    }

    public Task<IEnumerable<GetWeatherForecastResponse>> Handle(
#if (grpc)
        GetWeatherForecastGrpcRequest grpcRequest,
#else
        GetWeatherForecastRequest request,
#endif
        CancellationToken cancellationToken
    )
    {
#if (grpc)
    var request = grpcRequest.Request;
#endif

        return _actorProxyFactory
            .CreateActorProxy<IWeatherForecastActor>(
                ActorId.CreateRandom(),
                nameof(WeatherForecastActor)
            )
            .GetWeatherForecastAsync(request);
    }
}
