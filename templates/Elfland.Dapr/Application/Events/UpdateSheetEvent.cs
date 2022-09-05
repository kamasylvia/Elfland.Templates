namespace Elfland.Dapr.Application.Events;

public record class UpdateSheetEvent : INotification
{
    public Guid? Id { get; set; }
    public JsonDocument? Data { get; set; }
}
