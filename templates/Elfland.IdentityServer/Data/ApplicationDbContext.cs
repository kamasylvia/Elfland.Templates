using Microsoft.EntityFrameworkCore;

namespace Elfland.IdentityServer.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
