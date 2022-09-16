namespace Elfland.Dapr.Application.Events;

public record class UpdateSpreadsheetEvent : INotification
{
    [Required]
    public string? EventId { get; set; }
}
