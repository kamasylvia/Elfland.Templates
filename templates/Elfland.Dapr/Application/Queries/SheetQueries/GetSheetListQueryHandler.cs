using Elfland.Dapr.Application.Queries.SheetQueries;
using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Application.Queries.SheetQueries;

public class GetSheetListQueryHandler
    : IRequestHandler<GetSheetListQuery, IEnumerable<GetSheetResponse>>
{
    private readonly UnitOfWork _unitOfWork;

    public GetSheetListQueryHandler(
        ISpreadsheetRepository tableRepository,
        IMapper mapper,
        UnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<GetSheetResponse>> Handle(
        GetSheetListQuery request,
        CancellationToken cancellationToken
    ) =>
        _unitOfWork.Mapper.Map<IEnumerable<GetSheetResponse>>(
            await _unitOfWork.SpreadsheetRepository.SearchAsync()
        );
}
