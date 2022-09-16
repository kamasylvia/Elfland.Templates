namespace Elfland.Dapr.Application.Actors.Interfaces;

public interface ISpreadsheetActor : IActor
{
    Task UpdateSpreadsheetAsync(string? eventId);
}
