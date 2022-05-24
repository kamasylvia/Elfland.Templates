using Elfland.IdentityServer.Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Elfland.IdentityServer.Data;

public class ApplicationDbContext
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        NewId,
        IdentityUserClaim<NewId>,
        ApplicationUserRole,
        IdentityUserLogin<NewId>,
        IdentityRoleClaim<NewId>,
        IdentityUserToken<NewId>
    >
{
    private readonly IConfiguration _configuration;

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

        builder
            .Entity<ApplicationUser>()
            .Property<NewId>(user => user.Id)
            .HasConversion(newId => newId.ToString(), strId => new NewId(strId));

        builder
            .Entity<ApplicationRole>()
            .Property<NewId>(user => user.Id)
            .HasConversion(newId => newId.ToString(), strId => new NewId(strId));
    }
}
