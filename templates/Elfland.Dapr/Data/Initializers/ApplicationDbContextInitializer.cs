using Elfland.Dapr.Data.Entities;

namespace Elfland.Dapr.Data.Initializers;

public static class ApplicationDbContextInitializer
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var environment = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (environment.IsDevelopment())
        {
            var deleted = context.Database.EnsureDeleted();
            System.Console.WriteLine($"The old database is deleted: {deleted}");
            var created = context.Database.EnsureCreated();
            System.Console.WriteLine($"The new database is created: {created}");
        }
        else
        {
            context.Database.Migrate();
        }

        // Check database
        if (context?.WeatherForecasts?.Any() ?? true)
        {
            return; // Database does not exist or already been seeded.
        }

        await SeedDataAsync(context);
    }

    private static async Task SeedDataAsync(this ApplicationDbContext context)
    {
        // Add special data
        var weatherForecast = new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = -10,
            Summary = "Seed data"
        };

        await context.WeatherForecasts!.AddAsync(weatherForecast);
        await context.SaveChangesAsync();
    }
}
