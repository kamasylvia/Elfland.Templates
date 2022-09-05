namespace Elfland.Dapr.Application.Queries.SheetQueries;

public record class GetSheetQuery : IRequest<GetSheetResponse>
{
    public Guid? Id { get; set; }
}

public record class GetSheetResponse
{
    public JsonDocument? Data { get; set; }
}
