using System.Runtime.Serialization;

namespace Elfland.Dapr.Infrastructure.Exceptions;

public class NotFoundException : LogIgnoreException
{
    public override int? StateCode { get; set; } = StatusCodes.Status404NotFound;

    public NotFoundException() { }

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

}
