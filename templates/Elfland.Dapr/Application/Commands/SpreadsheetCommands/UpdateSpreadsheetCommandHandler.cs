using Elfland.Dapr.Application.Actors;
using Elfland.Dapr.Application.Actors.Interfaces;

namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public record class UpdateSpreadsheetCommandHandler : IRequestHandler<UpdateSpreadsheetCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public UpdateSpreadsheetCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(
        UpdateSpreadsheetCommand request,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.DaprClient.SaveStateAsync(
            UnitOfWork.DAPR_STORE_NAME,
            request.EventId,
            request
        );

        await _unitOfWork.ActorProxyFactory
            .CreateActorProxy<ISpreadsheetActor>(ActorId.CreateRandom(), nameof(SpreadsheetActor))
            .UpdateSpreadsheetAsync(request.EventId);

        return Unit.Value;
    }
}
