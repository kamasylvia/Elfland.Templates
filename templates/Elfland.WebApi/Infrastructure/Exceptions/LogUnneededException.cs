using System.Runtime.Serialization;

namespace Elfland.WebApi.Infrastructure.Exceptions;

public abstract class LogIgnoreException : HttpExceptionBase
{
    protected LogIgnoreException() { }

    protected LogIgnoreException(string message) : base(message) { }

    protected LogIgnoreException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
