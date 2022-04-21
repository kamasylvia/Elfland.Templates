using System.Runtime.Serialization;

namespace Elfland.Dapr.Infrastructure.Exceptions;

public abstract class LogUnneededException : HttpExceptionBase
{
    protected LogUnneededException() { }

    protected LogUnneededException(string message) : base(message) { }

    protected LogUnneededException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
