namespace Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;

public class Spreadsheet : EntityPixie, IAggregateRoot
{
    public List<Guid>? Sheets { get; set; }
}
