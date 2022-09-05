namespace Elfland.Dapr.Application.Commands.SheetCommands;

public record class UpdateSheetCommand : IRequest
{
    public Guid? Id { get; set; }
    public JsonDocument? Data { get; set; }
}
