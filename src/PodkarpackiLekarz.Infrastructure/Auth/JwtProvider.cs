using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PodkarpackiLekarz.Infrastructure.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private IOptions<AuthorizationSettings> _authorizationOptions;

        public JwtProvider(
            IOptions<AuthorizationSettings> authorizationOptions)
        {
            _authorizationOptions = authorizationOptions;
        }

        public string CreateAccessToken(
            IEnumerable<Claim> claims,
            DateTime currentDateTime,
            out DateTime expiresAt)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authorizationOptions.Value.SigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                _authorizationOptions.Value.Issuer,
                _authorizationOptions.Value.Audience,
                claims,
                expires: currentDateTime.AddMinutes(_authorizationOptions.Value.ExpirationMinutes),
                signingCredentials: credentials);
            expiresAt = tokenDescriptor.ValidTo;
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenDescriptor);
        }

        public string CreateRefreshToken(DateTime currentDateTIme, out DateTime validTo)
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                validTo = currentDateTIme.AddMinutes(_authorizationOptions.Value.RefreshTokenExpirationMinutes);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredAccessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorizationOptions.Value.SigningKey)),
                ValidateLifetime = false,
                ValidIssuer = _authorizationOptions.Value.Issuer,
                ValidAudience = _authorizationOptions.Value.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(expiredAccessToken, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new InvalidAccessTokenException();

                return principal;
            }
            catch (Exception e)
            {
                throw new InvalidAccessTokenException();
            }
        }
    }
}
