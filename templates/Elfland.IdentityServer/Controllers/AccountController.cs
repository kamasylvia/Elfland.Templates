using Elfland.IdentityServer.Application.Commands.AccountCommands;
using Elfland.IdentityServer.Data;
using Elfland.IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hollastin.Server.Controllers;

[Authorize]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _applicationDbContext;
    private static bool _databaseChecked;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user != null)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        var result = await _userManager.CreateAsync(new ApplicationUser { UserName = request.Username }, request.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
}