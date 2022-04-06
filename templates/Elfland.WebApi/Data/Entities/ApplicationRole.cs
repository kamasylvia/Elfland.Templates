using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Elfland.WebApi.Database.Entities;

public class ApplicationRole : IdentityRole<NewId>
{
    public virtual IEnumerable<ApplicationUserRole>? ApplicationUserRoles { get; set; }
}
