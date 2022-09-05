using Elfland.Dapr.Application.Actors;

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
        await _unitOfWork.ActorProxyFactory
            .CreateActorProxy<SpreadsheetActor>(ActorId.CreateRandom(), nameof(SpreadsheetActor))
            .UpdateSpreadsheetAsync(request);

        return Unit.Value;
    }
}
