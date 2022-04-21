using Microsoft.AspNetCore.Mvc;

namespace Elfland.Dapr.Infrastructure.ActionResults;

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object? value) : base(value)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
