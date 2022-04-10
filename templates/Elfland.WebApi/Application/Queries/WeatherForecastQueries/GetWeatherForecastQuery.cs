using MediatR;

namespace Elfland.WebApi.Application.Queries.WeatherForecastQueries;

public record GetWeatherForecastRequest : IRequest<IEnumerable<GetWeatherForecastResponse>> { }

public record GetWeatherForecastResponse
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}
