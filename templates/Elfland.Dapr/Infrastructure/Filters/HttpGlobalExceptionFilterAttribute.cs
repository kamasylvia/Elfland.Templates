using System.Net;
using Elfland.Dapr.Infrastructure.ActionResults;
using Elfland.Dapr.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elfland.Dapr.Infrastructure.Filters;

public class HttpGlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HttpGlobalExceptionFilterAttribute> _logger;

    public HttpGlobalExceptionFilterAttribute(
        IWebHostEnvironment env,
        ILogger<HttpGlobalExceptionFilterAttribute> logger
    )
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnException(ExceptionContext context)
    {
        var json = new JsonErrorResponse
        {
            Messages = new[] { context.Exception.Message ?? "An error occur.Try it again." }
        };

        if (_env.IsDevelopment())
        {
            json.DeveloperMessage = context.Exception;
        }

        switch (context.Exception)
        {
            case LogIgnoreException ex:
                switch (ex)
                {
                    case BadRequestException:
                        var problemDetails = new ValidationProblemDetails()
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status400BadRequest,
                            Detail = "Please refer to the errors property for additional details."
                        };
                        problemDetails.Errors.Add(
                            "DomainValidations",
                            new string[] { ex.Message ?? "Bad request." }
                        );
                        context.Result = new BadRequestObjectResult(problemDetails);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException:
                        context.Result = new NotFoundObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ConflictException:
                        context.Result = new ConflictObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                }
                break;
            default:
                string methodInfo =
                    $"{context.RouteData.Values["controller"] as string}Controller.{context.RouteData.Values["action"] as string}:{context.HttpContext.Request.Method}";

                _logger.LogError(
                    context.Exception,
                    $"Unhandled exception occurred while executing request: {methodInfo}"
                );

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(context.Exception.Message);
                break;
        }
        context.ExceptionHandled = true;
    }
}
