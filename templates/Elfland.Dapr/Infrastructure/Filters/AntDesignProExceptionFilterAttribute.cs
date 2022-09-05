using Elfland.Dapr.Infrastructure.ActionResults;
using Elfland.Dapr.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkshopDatabase.API.Infrastructure.Filters;

public class AntDesignProExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<AntDesignProExceptionFilterAttribute> _logger;

    public AntDesignProExceptionFilterAttribute(
        ILogger<AntDesignProExceptionFilterAttribute> logger
    )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        string methodInfo =
            $"{context.RouteData.Values["controller"] as string}Controller.{context.RouteData.Values["action"] as string}:{context.HttpContext.Request.Method}";

        _logger.LogError(
            context.Exception,
            $"Unhandled exception occurred while executing request: {methodInfo}"
        );

        var response = new AntDesignProApiResult
        {
            Success = false,
            ErrorCode = context.Exception.GetType().Name,
            ErrorMessage = context.Exception.Message,
            TraceId = context.HttpContext.TraceIdentifier,
            Host = context.HttpContext.Request.Host.ToString(),
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = context.Exception switch
            {
                ConflictException => StatusCodes.Status409Conflict,
                NotFoundException => StatusCodes.Status404NotFound,
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            }
        };

        await base.OnExceptionAsync(context);
    }
}
