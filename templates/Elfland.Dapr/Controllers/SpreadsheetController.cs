using Elfland.Dapr.Application.Commands.SpreadsheetCommands;
using Elfland.Dapr.Application.Events;
using Elfland.Dapr.Application.Queries.SpreadsheetQueries;

namespace Elfland.Dapr.Controllers;

[ApiController]
[Route("[controller]")]
public class SpreadsheetController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";
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

    [HttpPut]
    public async Task<IActionResult> UpdateSpreadsheetAsync(
        [FromBody] UpdateSpreadsheetCommand request
    )
    {
        await _mediator.Send(request);
        return Accepted();
    }

    [HttpPut("event")]
    [Topic(DAPR_PUBSUB_NAME, nameof(UpdateSpreadsheetEvent))]
    public async Task<IActionResult> UpdateSpreadsheetEventAsync(
        [FromBody] UpdateSpreadsheetCommand request
    )
    {
        await _mediator.Send(request);
        return Accepted();
    }
}
