using Elfland.Dapr.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elfland.Dapr.Infrastructure.Filters;

public class ActionPixieAttribute : ActionFilterAttribute
{
    public override async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next
    )
    {
        switch (context.Result)
        {
            case ObjectResult objectResult:
                objectResult.Value = new ApiResult
                {
                    StatusCode = objectResult.StatusCode,
                    Data = objectResult.Value,
                    Succeeded = true
                };
                break;
            case OkResult okResult:
                context.Result = new ObjectResult(
                    new ApiResult
                    {
                        StatusCode = okResult.StatusCode,
                        Data = "Ok",
                        Succeeded = true
                    }
                );
                break;
            default:
                break;
        }
        await next();
    }
}
