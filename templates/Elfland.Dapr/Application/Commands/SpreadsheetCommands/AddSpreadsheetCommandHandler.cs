using Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;

namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public class AddSpreadsheetCommandHandler : IRequestHandler<AddSpreadsheetCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public AddSpreadsheetCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(
        AddSpreadsheetCommand request,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.SpreadsheetRepository.AddAsync(
            _unitOfWork.Mapper.Map<Spreadsheet>(request)
        );

        await _unitOfWork.SaveAndCommitAsync();

        return Unit.Value;
    }
}
