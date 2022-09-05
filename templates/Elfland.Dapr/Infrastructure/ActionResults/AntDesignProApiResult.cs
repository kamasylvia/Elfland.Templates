namespace Elfland.Dapr.Infrastructure.ActionResults;

public class AntDesignProApiResult
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public int? ShowType { get; set; }
    public string? TraceId { get; set; }
    public string? Host { get; set; }
}
