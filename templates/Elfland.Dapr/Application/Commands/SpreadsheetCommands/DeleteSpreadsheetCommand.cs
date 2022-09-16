namespace Elfland.Dapr.Application.Commands.SpreadsheetCommands;

public record class DeleteSpreadsheetCommand : IRequest
{
    [Required]
    public Guid? Id { get; set; }
}
