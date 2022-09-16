namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public record class UpdateSpreadsheetCommand : IRequest
{
    [Required]
    public Guid? Id { get; set; }
    public string? EventId { get; init; } = NewId.NextGuid().ToString();
    public JsonDocument? Data { get; set; }
}
