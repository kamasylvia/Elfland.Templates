using Elfland.Dapr.Application.Actors.Interfaces;
using Elfland.Dapr.Application.Events;

namespace Elfland.Dapr.Application.Actors;

public class SpreadsheetActor : Actor, ISpreadsheetActor
{
    private readonly UnitOfWork _unitOfWork;

    public SpreadsheetActor(ActorHost host, UnitOfWork unitOfWork) : base(host) =>
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task UpdateSpreadsheetAsync(string? eventId) =>
        await _unitOfWork.Mediator.Publish(new UpdateSpreadsheetEvent { EventId = eventId });
}
