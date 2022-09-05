namespace Elfland.Dapr.Application.Queries.SpreadsheetQueries;

public record class GetSpreadsheetQuery : IRequest<GetSpreadsheetResponse>
{
    public Guid? Id { get; set; }
}

public record class GetSpreadsheetResponse
{
    public JsonDocument? Data { get; set; }
}
