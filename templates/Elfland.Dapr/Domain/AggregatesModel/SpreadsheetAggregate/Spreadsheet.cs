namespace Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;

public class Spreadsheet : EntityPixie, IAggregateRoot
{
    public JsonDocument? Data { get; set; }
}
