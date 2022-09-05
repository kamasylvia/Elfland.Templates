using Elfland.Dapr.Application.Actors;

namespace Elfland.Dapr.Application.Commands.SheetCommands;

public class UpdateSheetCommandHandler : IRequestHandler<UpdateSheetCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public UpdateSheetCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ActorProxyFactory
            .CreateActorProxy<SheetActor>(ActorId.CreateRandom(), nameof(SheetActor))
            .UpdateTableAsync(request);

        return Unit.Value;
    }
}
