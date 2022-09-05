using Elfland.Dapr.Domain.AggregatesModel.TableAggregate;
using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Infrastructure.Repositories;

public class SheetRepository : RepositoryBase<Sheet, ApplicationDbContext>, ISheetRepository
{
    public SheetRepository(ApplicationDbContext context) : base(context) { }
}
