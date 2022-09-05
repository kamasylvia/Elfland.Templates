using Elfland.Dapr.Infrastructure.Repositories.Interfaces;

namespace Elfland.Dapr.Application.Queries.SheetQueries;

public class GetSheetQueryHandler : IRequestHandler<GetSheetQuery, GetSheetResponse>
{
    private readonly UnitOfWork _unitOfWork;

    public GetSheetQueryHandler(
        ISheetRepository tableRepository,
        IMapper mapper,
        UnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<GetSheetResponse> Handle(
        GetSheetQuery request,
        CancellationToken cancellationToken
    ) =>
        _unitOfWork.Mapper.Map<GetSheetResponse>(
            await _unitOfWork.TableRepository.FindByIdAsync(request.Id!, cancellationToken)
        );
}
