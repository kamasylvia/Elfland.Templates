using AutoMapper;
using Elfland.WebApi.Data.Entities;
using MediatR;

namespace Elfland.Dapr.Application.Queries.WeatherForecastQueries;

public class GetWeatherForecastQueryHandler
    : IRequestHandler<GetWeatherForecastRequest, IEnumerable<GetWeatherForecastResponse>>
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

    public GetWeatherForecastQueryHandler(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<GetWeatherForecastResponse>> Handle(
        GetWeatherForecastRequest request,
        CancellationToken cancellationToken
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
