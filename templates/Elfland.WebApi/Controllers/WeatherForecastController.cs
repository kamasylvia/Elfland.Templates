using Elfland.WebApi.Application.Queries.WeatherForecastQueries;

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
    public async Task<IEnumerable<GetWeatherForecastResponse>> Get(
        [FromQuery] GetWeatherForecastRequest request
    ) =>
        request.Message?.ToLower().Contains("exception") ?? false
            ? throw new Exception()
            : await _mediator.Send(request);
}
