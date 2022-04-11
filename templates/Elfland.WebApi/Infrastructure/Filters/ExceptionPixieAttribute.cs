using Elfland.WebApi.Infrastructure.ActionResults;
using Elfland.WebApi.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elfland.WebApi.Infrastructure.Filters;

public class ExceptionPixieAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionPixieAttribute> _logger;

    public ExceptionPixieAttribute(ILogger<ExceptionPixieAttribute> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnException(ExceptionContext context)
    {
        string methodInfo =
            $"{context.RouteData.Values["controller"] as string}Controller.{context.RouteData.Values["action"] as string}:{context.HttpContext.Request.Method}";

        _logger.LogError(
            context.Exception,
            $"Unhandled exception occurred while executing request: {methodInfo}"
        );

        var apiResult = new ApiResult { Succeeded = false };

        ExceptionHandler(context.Exception, apiResult);

        _logger.LogError(apiResult.Errors as string);
        context.Result = new ObjectResult(apiResult) { StatusCode = apiResult.StatusCode };
    }

    private void ExceptionHandler(Exception? exception, ApiResult apiResult)
    {
        switch (exception)
        {
            case BadRequestException ex:
                apiResult.StatusCode = ex.StateCode;
                apiResult.Errors = ex.Message ?? "Bad request error.";
                break;
            case NotFoundException ex:
                apiResult.StatusCode = ex.StateCode;
                apiResult.Errors = ex.Message ?? "Could not find the entity.";
                break;
            case DatabaseUpdateException ex:
                apiResult.StatusCode = ex.StateCode;
                apiResult.Errors = ex.Message ?? "Database Update Error.";
                break;
            case RemoteServiceException ex:
                apiResult.StatusCode = ex.StateCode;
                apiResult.Errors = ex.Message ?? "Remote service error.";
                break;
            case ConflictException ex:
                apiResult.StatusCode = ex.StateCode;
                apiResult.Errors = ex.Message ?? "Conflict error.";
                break;
            default:
                apiResult.StatusCode = StatusCodes.Status500InternalServerError;
                apiResult.Errors = "Internal Server Error.";
                break;
        }
    }
}
