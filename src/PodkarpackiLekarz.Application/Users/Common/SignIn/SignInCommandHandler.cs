using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.SignIn;
public class SignInCommandHandler : IRequestHandler<SignInCommand>
{
    private readonly ICookieAuthorizationService _cookieAuthorizationService;

    public SignInCommandHandler(ICookieAuthorizationService cookieAuthorizationService)
    {
        _cookieAuthorizationService = cookieAuthorizationService;
    }

    public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        await _cookieAuthorizationService.SignInAsync(request.Email, request.Password);
    }
}
