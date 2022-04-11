using System.Runtime.Serialization;

namespace Elfland.WebApi.Infrastructure.Exceptions;

public abstract class HttpExceptionBase : Exception
{
    public abstract int? StateCode { get; set; }

    protected HttpExceptionBase() { }

    protected HttpExceptionBase(string message) : base(message) { }

    protected HttpExceptionBase(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
