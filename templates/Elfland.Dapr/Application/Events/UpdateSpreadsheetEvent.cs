namespace Elfland.Dapr.Application.Events;

public record class UpdateSpreadsheetEvent : INotification
{
    public Guid? Id { get; set; }
    public JsonDocument? Data { get; set; }
}
