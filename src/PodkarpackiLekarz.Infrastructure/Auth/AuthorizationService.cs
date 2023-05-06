using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Shared.Identity;
using System.Security.Claims;

namespace PodkarpackiLekarz.Infrastructure.Auth
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IIdentityUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher<Core.Users.IdentityUser> _passwordHasher;        

        public AuthorizationService(
            IIdentityUsersRepository usersRepository,
            IJwtProvider jwtProvider,
            IPasswordHasher<Core.Users.IdentityUser> passwordHasher)
        {
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;            
        }

        public async Task<AuthDto> RefreshTokenAsync(string expiredAccessToken, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(expiredAccessToken) || string.IsNullOrWhiteSpace(refreshToken))
                throw new InvalidRefreshTokenException();

            var principal = _jwtProvider.GetPrincipalFromExpiredToken(expiredAccessToken);

            if (principal.Claims is null || !principal.Claims.Any())
                throw new InvalidAccessTokenException();

            var userGuid = principal.Claims.
                FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userGuid is null)
                throw new InvalidAccessTokenException();
            
            var userId = Guid.Parse(userGuid!);

            var user = await _usersRepository.GetAsync(userId);

            if (user is null
                || user.Session?.RefreshToken != refreshToken
                || user.Session.RefreshTokenExpiryDate <= DateTime.UtcNow)
                throw new InvalidRefreshTokenException();

            var currentDateTime = DateTime.UtcNow;

            var newAccessToken = _jwtProvider.CreateAccessToken(
                principal.Claims.ToList(),
                currentDateTime, out var expiresAt);

            var newRefreshToken = _jwtProvider.CreateRefreshToken(
                currentDateTime,
                out var validTo);

            user.SetSession(newRefreshToken, validTo);

            _usersRepository.UpdateAsync(user);
            await _usersRepository.SaveAsync();

            var authDto = new AuthDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = expiresAt
            };

            return authDto;
        }

        public async Task<AuthDto> SignInAsync(string email, string password)
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
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var permissions = Permissions.GetPermissions(user.Role);

            claims.AddRange(permissions.Select(permission => new Claim("permissions", permission)));

            var currentDateTime = DateTime.UtcNow;

            var accessToken = _jwtProvider.CreateAccessToken(claims, currentDateTime, out DateTime expiresAt);
            var refreshToken = _jwtProvider.CreateRefreshToken(currentDateTime, out DateTime validTo);

            user.SetSession(refreshToken, validTo);

            _usersRepository.UpdateAsync(user);
            await _usersRepository.SaveAsync();

            return new AuthDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt
            };
        }
    }
}
