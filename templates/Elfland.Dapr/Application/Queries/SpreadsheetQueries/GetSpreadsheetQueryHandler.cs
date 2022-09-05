using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Application.Queries.SpreadsheetQueries;

public class GetSpreadsheetQueryHandler
    : IRequestHandler<GetSpreadsheetQuery, GetSpreadsheetResponse>
{
    private readonly UnitOfWork _unitOfWork;

    public GetSpreadsheetQueryHandler(
        ISpreadsheetRepository spreadsheetRepository,
        IMapper mapper,
        UnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<GetSpreadsheetResponse> Handle(
        GetSpreadsheetQuery request,
        CancellationToken cancellationToken
    ) =>
        _unitOfWork.Mapper.Map<GetSpreadsheetResponse>(
            await _unitOfWork.SpreadsheetRepository.FindByIdAsync(request.Id!, cancellationToken)
        );
}
