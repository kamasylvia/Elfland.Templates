using MediatR;

namespace Elfland.IdentityServer.Application.Commands.AccountCommands;

public record RegisterCommand : IRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
