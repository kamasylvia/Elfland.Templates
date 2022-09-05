namespace Elfland.Dapr.Application.Events;

public class UpdateSheetEventHandler : INotificationHandler<UpdateSheetEvent>
{
    private readonly UnitOfWork _unitOfWork;

    public UpdateSheetEventHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Handle(UpdateSheetEvent notification, CancellationToken cancellationToken) =>
        _unitOfWork.TableRepository.Update(
            _unitOfWork.Mapper.Map(
                notification,
                await _unitOfWork.TableRepository.FindByIdAsync(notification.Id!)
            )!
        );
}
