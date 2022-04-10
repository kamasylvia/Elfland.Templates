using MediatR;

namespace Elfland.Dapr.Application.Queries.WeatherForecastQueries;

public record GetWeatherForecastRequest : IRequest<IEnumerable<GetWeatherForecastResponse>> { }

#if (grpc)
public record GetWeatherForecastGrpcRequest : IRequest<IEnumerable<GetWeatherForecastResponse>>
{
    public GetWeatherForecastRequest? Request { get; set; }
}
#endif

public record GetWeatherForecastResponse
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}
