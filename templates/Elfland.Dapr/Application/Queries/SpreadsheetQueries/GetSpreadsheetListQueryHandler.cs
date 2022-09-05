using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Application.Queries.SpreadsheetQueries;

public class GetSpreadsheetListQueryHandler
    : IRequestHandler<GetSpreadsheetListQuery, IEnumerable<GetSpreadsheetResponse>>
{
    private readonly UnitOfWork _unitOfWork;

    public GetSpreadsheetListQueryHandler(
        ISpreadsheetRepository spreadsheetRepository,
        IMapper mapper,
        UnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<GetSpreadsheetResponse>> Handle(
        GetSpreadsheetListQuery request,
        CancellationToken cancellationToken
    ) =>
        _unitOfWork.Mapper.Map<IEnumerable<GetSpreadsheetResponse>>(
            await _unitOfWork.SpreadsheetRepository.SearchAsync()
        );
}
