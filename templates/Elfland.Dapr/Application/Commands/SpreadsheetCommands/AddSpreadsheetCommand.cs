namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public record class AddSpreadsheetCommand : IRequest
{
    public JsonDocument? Data { get; set; }
}
