using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.SignOut
{
    public class SignOutCommandHandler
        : IRequestHandler<SignOutCommand>
    {
        private readonly ICookieAuthorizationService _authorizationService;

        public SignOutCommandHandler(ICookieAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
            => await _authorizationService.SignOutAsync();
    }
}
