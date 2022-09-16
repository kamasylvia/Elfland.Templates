using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Infrastructure;

public class UnitOfWork : UnitOfWorkPixie<ApplicationDbContext>
{
    public const string DAPR_STORE_NAME = "statestore";
    public DaprClient DaprClient { get; }
    public IActorProxyFactory ActorProxyFactory { get; }
    public IMapper Mapper { get; }
    public IMediator Mediator { get; }
    public IDomainEventBus DomainEventBus { get; }
    public ISpreadsheetRepository SpreadsheetRepository { get; }

    public UnitOfWork(
        DaprClient daprClient,
        IActorProxyFactory actorProxyFactory,
        IMapper mapper,
        IMediator mediator,
        IDomainEventBus domainEventBus,
        ISpreadsheetRepository spreadsheetRepository,
        ApplicationDbContext context
    ) : base(context)
    {
        DaprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));

        ActorProxyFactory =
            actorProxyFactory ?? throw new ArgumentNullException(nameof(actorProxyFactory));

        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        DomainEventBus = domainEventBus ?? throw new ArgumentNullException(nameof(domainEventBus));

        SpreadsheetRepository =
            spreadsheetRepository ?? throw new ArgumentNullException(nameof(spreadsheetRepository));
    }
}
