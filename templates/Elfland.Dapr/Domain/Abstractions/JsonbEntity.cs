namespace Elfland.Dapr.Domain.Abstractions;

public abstract class JsonbEntity : EntityPixie, IDisposable
{
    public JsonDocument? Data { get; set; }

    public void Dispose() => Data?.Dispose();
}
