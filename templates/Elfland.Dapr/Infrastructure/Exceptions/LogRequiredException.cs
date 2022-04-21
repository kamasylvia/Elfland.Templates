using System.Runtime.Serialization;

namespace Elfland.Dapr.Infrastructure.Exceptions;

public abstract class LogRequiredException : HttpExceptionBase
{
    public LogRequiredException() { }

    public LogRequiredException(string message) : base(message) { }

    public LogRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
