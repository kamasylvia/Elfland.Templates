using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Infrastructure;

public class UnitOfWork : UnitOfWorkPixie<ApplicationDbContext>
{
    public IActorProxyFactory ActorProxyFactory { get; }
    public IMapper Mapper { get; }
    public IMediator Mediator { get; }
    public IDomainEventBus DomainEventBus { get; }
    public ISheetRepository TableRepository { get; }
    public ISpreadsheetRepository SpreadsheetRepository { get; }

    public UnitOfWork(
        IActorProxyFactory actorProxyFactory,
        IMapper mapper,
        IMediator mediator,
        IDomainEventBus domainEventBus,
        ISheetRepository tableRepository,
        ISpreadsheetRepository spreadsheetRepository,
        ApplicationDbContext context
    ) : base(context)
    {
        ActorProxyFactory =
            actorProxyFactory ?? throw new ArgumentNullException(nameof(actorProxyFactory));

        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        DomainEventBus = domainEventBus ?? throw new ArgumentNullException(nameof(domainEventBus));

        TableRepository =
            tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));

        SpreadsheetRepository =
            spreadsheetRepository ?? throw new ArgumentNullException(nameof(spreadsheetRepository));
    }
}
