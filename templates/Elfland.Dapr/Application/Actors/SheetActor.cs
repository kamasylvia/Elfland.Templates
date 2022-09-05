using Elfland.Dapr.Application.Commands.SheetCommands;
using Elfland.Dapr.Application.Events;

namespace Elfland.Dapr.Application.Actors;

public class SheetActor : Actor, IActor
{
    private readonly UnitOfWork _unitOfWork;

    public SheetActor(ActorHost host, UnitOfWork unitOfWork) : base(host)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task UpdateTableAsync(UpdateSheetCommand request) =>
        await _unitOfWork.Mediator.Publish(_unitOfWork.Mapper.Map<UpdateSheetEvent>(request));
}
