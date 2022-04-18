using Elfland.WebApi.Application.Queries.WeatherForecastQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elfland.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IEnumerable<GetWeatherForecastResponse>> Get() =>
        await _mediator.Send(new GetWeatherForecastRequest());
}
