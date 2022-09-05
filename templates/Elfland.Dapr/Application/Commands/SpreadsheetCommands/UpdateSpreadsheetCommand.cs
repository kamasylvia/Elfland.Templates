namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public record class UpdateSpreadsheetCommand : IRequest
{
    public Guid? Id { get; set; }
    public JsonDocument? Data { get; set; }
}
