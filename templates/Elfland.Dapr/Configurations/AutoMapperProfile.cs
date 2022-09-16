using Elfland.Dapr.Application.Commands.SpreadsheetCommands;
using Elfland.Dapr.Application.Queries.SpreadsheetQueries;
using Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;

namespace Elfland.Dapr.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Spreadsheet, GetSpreadsheetResponse>();
        CreateMap<AddSpreadsheetCommand, Spreadsheet>();
        CreateMap<UpdateSpreadsheetCommand, Spreadsheet>();
    }
}
