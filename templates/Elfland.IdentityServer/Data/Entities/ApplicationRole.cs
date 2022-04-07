using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Elfland.IdentityServer.Data.Entities;

public class ApplicationRole : IdentityRole<NewId>
{
    public virtual IEnumerable<ApplicationUserRole>? ApplicationUserRoles { get; set; }
}
