namespace Elfland.WebApi.Data.Entities;

public class WeatherForecast
{
    [Key]
    public Guid? Id { get; set; } = NewId.NextGuid();

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
