using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.SignIn;
public class SignInCommandHandler : IRequestHandler<SignInCommand, AuthDto>
{
    private readonly IAuthorizationService _authorizationService;

    public SignInCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<AuthDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var authDto = await _authorizationService.SignInAsync(
            request.Email,
            request.Password);

        return authDto;
    }
}
