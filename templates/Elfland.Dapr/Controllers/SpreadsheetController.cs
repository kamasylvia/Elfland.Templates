using Elfland.Dapr.Application.Commands.SpreadsheetCommands;
using Elfland.Dapr.Application.Queries.SpreadsheetQueries;

namespace Elfland.Dapr.Controllers;

[ApiController]
[Route("[controller]")]
public class SpreadsheetController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpreadsheetController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<GetSpreadsheetResponse>> GetSpreadsheetAsync(
        [FromQuery] GetSpreadsheetQuery request
    ) => Ok(await _mediator.Send(request));

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<GetSpreadsheetResponse>>> GetSpreadsheetListAsync(
        [FromQuery] GetSpreadsheetListQuery request
    ) => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AddSpreadsheetAsync([FromBody] AddSpreadsheetCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpreadsheetAsync(
        [FromBody] UpdateSpreadsheetCommand request
    )
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpreadsheetAsync(
        [FromBody] DeleteSpreadsheetCommand request
    )
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
