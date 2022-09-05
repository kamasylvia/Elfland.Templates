using Elfland.Dapr.Application.Commands.SpreadsheetCommands;
using Elfland.Dapr.Application.Commands.SheetCommands;
using Elfland.Dapr.Application.Events;
using Elfland.Dapr.Application.Queries.SpreadsheetQueries;
using Elfland.Dapr.Application.Queries.SheetQueries;
using Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;
using Elfland.Dapr.Domain.AggregatesModel.TableAggregate;

namespace Elfland.Dapr.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Spreadsheet, GetSpreadsheetResponse>();
        CreateMap<UpdateSpreadsheetCommand, UpdateSpreadsheetEvent>();
        CreateMap<UpdateSpreadsheetEvent, Spreadsheet>();

        CreateMap<Sheet, GetSheetResponse>();
        CreateMap<UpdateSheetCommand, UpdateSheetEvent>();
        CreateMap<UpdateSheetEvent, Sheet>();
    }
}
