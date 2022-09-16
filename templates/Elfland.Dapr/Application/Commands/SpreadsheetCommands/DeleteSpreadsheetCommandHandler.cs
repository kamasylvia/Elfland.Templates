namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public class DeleteSpreadsheetCommandHandler : IRequestHandler<DeleteSpreadsheetCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public DeleteSpreadsheetCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(
        DeleteSpreadsheetCommand request,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.SpreadsheetRepository.DeleteByIdAsync(request.Id!);

        await _unitOfWork.SaveAndCommitAsync();

        return Unit.Value;
    }
}
