using Elfland.Dapr.Application.Commands.SheetCommands;
using Elfland.Dapr.Application.Events;
using Elfland.Dapr.Application.Queries.SheetQueries;

namespace Elfland.Dapr.Controllers;

[ApiController]
[Route("[controller]")]
public class SheetController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";
    private readonly IMediator _mediator;

    public SheetController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<GetSheetResponse>> GetSheetAsync(
        [FromQuery] GetSheetQuery request
    ) => Ok(await _mediator.Send(request));

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<GetSheetResponse>>> GetTableListAsync(
        [FromQuery] GetSheetListQuery request
    ) => Ok(await _mediator.Send(request));

    [HttpPut]
    public async Task<IActionResult> UpdateSheetAsync([FromBody] UpdateSheetCommand request)
    {
        await _mediator.Send(request);
        return Accepted();
    }

    [HttpPut("event")]
    [Topic(DAPR_PUBSUB_NAME, nameof(UpdateSheetEvent))]
    public async Task<IActionResult> UpdateSheetEventAsync([FromBody] UpdateSheetCommand request)
    {
        await _mediator.Send(request);
        return Accepted();
    }
}
