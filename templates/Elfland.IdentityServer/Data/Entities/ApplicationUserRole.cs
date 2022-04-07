using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Elfland.IdentityServer.Data.Entities;

public class ApplicationUserRole : IdentityUserRole<NewId>
{
    public virtual ApplicationUser? ApplicationUsers { get; set; }
    public virtual ApplicationRole? ApplicationRoles { get; set; }
}
