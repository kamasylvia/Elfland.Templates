namespace Elfland.Dapr.Application.Queries.SpreadsheetQueries;

public record class GetSpreadsheetListQuery : IRequest<IEnumerable<GetSpreadsheetResponse>>;
