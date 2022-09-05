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
    ) =>
        _unitOfWork.SpreadsheetRepository.Update(
            _unitOfWork.Mapper.Map(
                notification,
                await _unitOfWork.SpreadsheetRepository.FindByIdAsync(notification.Id!)
            )!
        );
}
