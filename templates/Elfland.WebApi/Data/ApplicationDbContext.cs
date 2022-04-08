using Elfland.WebApi.Data.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Elfland.WebApi.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    // Entity sets
    public virtual DbSet<EntityExample>? EntityExamples { get; set; }

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

#if (postgres)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
#elif (mssql)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MsSQL"));
#endif

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<EntityExample>().Property<NewId>(entity => entity.Id).HasConversion(
            newId => newId.ToString(),
            stringId => new NewId(stringId)
        );
    }
}
