using System.Text.Json;
using Elfland.IdentityServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elfland.IdentityServer.Data.Initializers;

public static class ApplicationDbContextInitializer
{
    private static List<ApplicationRole>? _roles = JsonSerializer.Deserialize<
        List<ApplicationRole>
    >(System.IO.File.ReadAllText("Data/Initializers/DefaultRoles.json"));
    private static List<ApplicationUser>? _users = JsonSerializer.Deserialize<
        List<ApplicationUser>
    >(System.IO.File.ReadAllText("Data/Initializers/DefaultUsers.json"));

    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
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
        // if (context?.Users?.Any() ?? true)
        // {
        //     return; // Database does not exist or already been seeded.
        // }

        await SeedDataAsync(context);
    }

    private static async Task SeedDataAsync(this ApplicationDbContext context)
    {
        // Add special data
        // await context.Users!.AddRangeAsync(_users!);
        // await context.Roles!.AddRangeAsync(_roles!);
        // await context.SaveChangesAsync();
    }
}
