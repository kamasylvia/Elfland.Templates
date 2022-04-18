using System.Runtime.Serialization;

namespace Elfland.WebApi.Infrastructure.Exceptions;

public class DatabaseUpdateException : LogRequiredException
{
    public override int? StateCode { get; set; } = StatusCodes.Status500InternalServerError;

    public DatabaseUpdateException() { }

    public DatabaseUpdateException(string message) : base(message) { }

    public DatabaseUpdateException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
