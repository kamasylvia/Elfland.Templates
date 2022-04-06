namespace Elfland.WebApi.Data.Initializers;

public static class ApplicationDbContextInitializer
{
    public static async Task SeedDataAsync(
        this IServiceProvider serviceProvider,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}
