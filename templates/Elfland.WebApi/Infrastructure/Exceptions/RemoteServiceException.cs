using System.Runtime.Serialization;

namespace Elfland.WebApi.Infrastructure.Exceptions;

public class RemoteServiceException : LogRequiredException
{
    public override int? StateCode { get; set; } = StatusCodes.Status503ServiceUnavailable;

    public RemoteServiceException() { }

    public RemoteServiceException(string message) : base(message) { }

    public RemoteServiceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
