namespace Elfland.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [Authorize]
    public ActionResult Login()
    {
    }

    [Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
    Roles = "admin"
)]

    public ActionResult Logout()
    {
    }
}
