using Elfland.Dapr.Application.Commands.SpreadsheetCommands;

namespace Elfland.Dapr.Application.Events;

public class UpdateSpreadsheetEventHandler : INotificationHandler<UpdateSpreadsheetEvent>
{
    private readonly UnitOfWork _unitOfWork;

    public UpdateSpreadsheetEventHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Handle(
        UpdateSpreadsheetEvent notification,
        CancellationToken cancellationToken
    )
    {
        var updateSpreadsheetCommand =
            await _unitOfWork.DaprClient.GetStateAsync<UpdateSpreadsheetCommand>(
                UnitOfWork.DAPR_STORE_NAME,
                notification.EventId
            );

        _unitOfWork.SpreadsheetRepository.Update(
            _unitOfWork.Mapper.Map(
                updateSpreadsheetCommand,
                await _unitOfWork.SpreadsheetRepository.FindByIdAsync(updateSpreadsheetCommand.Id!)
            )!
        );

        if (await _unitOfWork.SaveAndCommitAsync())
        {
            await _unitOfWork.DaprClient.DeleteStateAsync(
                UnitOfWork.DAPR_STORE_NAME,
                notification.EventId
            );
        }
    }
}
