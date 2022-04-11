using System.Runtime.Serialization;

namespace Elfland.WebApi.Infrastructure.Exceptions;

public class NotFoundException : HttpExceptionBase
{
    public override int? StateCode { get; set; } = StatusCodes.Status404NotFound;

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}
