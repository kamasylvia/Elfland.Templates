namespace Elfland.Dapr.Application.Queries.SheetQueries;

public record class GetSheetListQuery : IRequest<IEnumerable<GetSheetResponse>>;
