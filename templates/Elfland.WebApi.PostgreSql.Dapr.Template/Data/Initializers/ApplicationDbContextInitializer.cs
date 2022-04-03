namespace Elfland.WebApi.PostgreSql.Dapr.Template.Data.Initializers;

public static class ApplicationDbContextInitializer
{
    public static async Task InitializeDbContextAsync(
        this IServiceProvider serviceProvider,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}

