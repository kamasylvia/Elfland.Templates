using System.Runtime.Serialization;

namespace Elfland.Dapr.Infrastructure.Exceptions;

public class BadRequestException : HttpExceptionBase
{
    public override int? StateCode { get; set; } = StatusCodes.Status400BadRequest;

    public BadRequestException() { }

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
