using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Shared.Identity;
using System.Security.Claims;

namespace PodkarpackiLekarz.Infrastructure.Auth
{
    public class CookieAuthorizationService : ICookieAuthorizationService
    {
        private readonly IIdentityUsersRepository _usersRepository;
        private readonly IPasswordHasher<Core.Users.Base.IdentityUser> _passwordHasher;
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieAuthorizationService(
            IIdentityUsersRepository usersRepository,
            IPasswordHasher<Core.Users.Base.IdentityUser> passwordHasher,
            IHttpContextAccessor contextAccessor)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _contextAccessor = contextAccessor;
        }

        public async Task SignInAsync(string email, string password)
        {
            var user = await _usersRepository.GetAsync(email);

            if (user is null)
                throw new InvalidCredentialsException();

            var passwordVerificationResult = _passwordHasher
                .VerifyHashedPassword(user, user.Password!, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new InvalidCredentialsException();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var permissions = Permissions.GetPermissions(user.Role);

            claims.AddRange(permissions.Select(permission => new Claim("permissions", permission)));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _contextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
        public async Task SignOutAsync()
        {
            await _contextAccessor.HttpContext!.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
