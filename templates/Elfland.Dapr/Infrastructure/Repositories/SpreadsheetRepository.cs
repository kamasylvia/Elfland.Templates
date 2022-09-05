using Elfland.Dapr.Domain.AggregatesModel.ApplicationAggregates;
using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Infrastructure.Repositories;

[ApplicationService(ServiceLifetime.Scoped)]
public class SpreadsheetRepository
    : RepositoryBase<Spreadsheet, ApplicationDbContext>,
        ISpreadsheetRepository
{
    public SpreadsheetRepository(ApplicationDbContext context) : base(context) { }
}
