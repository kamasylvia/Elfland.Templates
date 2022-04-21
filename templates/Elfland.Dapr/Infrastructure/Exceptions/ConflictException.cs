using System.Runtime.Serialization;

namespace Elfland.Dapr.Infrastructure.Exceptions;

public class ConflictException : LogUnneededException
{
    public override int? StateCode { get; set; } = StatusCodes.Status409Conflict;

    public ConflictException() { }

    public ConflictException(string message) : base(message) { }

    public ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}
