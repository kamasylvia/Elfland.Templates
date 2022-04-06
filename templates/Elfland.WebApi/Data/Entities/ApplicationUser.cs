using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Elfland.WebApi.Database.Entities;
public class ApplicationUser : IdentityUser<NewId>
{
    public virtual IEnumerable<ApplicationUserRole>? ApplicationUserRoles { get; set; }
}
