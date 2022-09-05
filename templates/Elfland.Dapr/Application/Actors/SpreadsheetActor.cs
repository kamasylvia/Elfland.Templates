using Elfland.Dapr.Application.Commands.SpreadsheetCommands;
using Elfland.Dapr.Application.Events;

namespace Elfland.Dapr.Application.Actors;

public class SpreadsheetActor : Actor, IActor
{
    private readonly UnitOfWork _unitOfWork;

    public SpreadsheetActor(ActorHost host, UnitOfWork unitOfWork) : base(host)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task UpdateSpreadsheetAsync(UpdateSpreadsheetCommand request) =>
        await _unitOfWork.Mediator.Publish(_unitOfWork.Mapper.Map<UpdateSpreadsheetEvent>(request));
}
