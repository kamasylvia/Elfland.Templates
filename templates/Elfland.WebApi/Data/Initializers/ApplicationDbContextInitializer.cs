using Elfland.WebApi.Data.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Elfland.WebApi.Data.Initializers;

public static class ApplicationDbContextInitializer
{
    public static async Task InitializeDatabaseAsync(
        this IServiceProvider serviceProvider
    )
    {
        using var serviceScope = serviceProvider.CreateScope();
        var environment = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (Convert.ToBoolean(configuration["RefreshDbEveryTime"]))
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
        if (context?.EntityExamples?.Any() ?? true)
        {
            return; // Database does not exist or already been seeded.
        }

        await SeedDataAsync(context);
    }

    private static async Task SeedDataAsync(
        this ApplicationDbContext context
    )
    {
        // Add special data
        var entityExample = new EntityExample
        {
            Id = NewId.Next(),
            Name = "Seeded Entity Example"
        };

        await context.EntityExamples!.AddAsync(entityExample);
        await context.SaveChangesAsync();
    }
}
