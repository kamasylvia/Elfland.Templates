using Elfland.Dapr.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elfland.Dapr.Infrastructure.Filters;

public class AntDesignProResponseFilterAttribute : ActionFilterAttribute
{
    public override async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next
    )
    {
        switch (context.Result)
        {
            case OkResult:
                context.Result = new ObjectResult(new AntDesignProApiResult { Success = true, })
                {
                    StatusCode = StatusCodes.Status200OK
                };
                break;
            case CreatedResult:
                context.Result = new ObjectResult(new AntDesignProApiResult { Success = true, })
                {
                    StatusCode = StatusCodes.Status201Created
                };
                break;
            case AcceptedResult:
                context.Result = new ObjectResult(new AntDesignProApiResult { Success = true, })
                {
                    StatusCode = StatusCodes.Status202Accepted
                };
                break;
            case ObjectResult objectResult:
                objectResult.Value = new AntDesignProApiResult
                {
                    Success = objectResult.StatusCode == StatusCodes.Status200OK,
                    Data = objectResult.Value,
                };
                break;
            default:
                break;
        }

        await next();
    }
}
