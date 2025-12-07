using ConfHub.Core.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConfHub.Core.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;
        private readonly int _expiryMinutes;

        public TokenService(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"] ?? "ConfHub";
            _audience = configuration["Jwt:Audience"] ?? "ConfHub.Api";
            _key = Environment.GetEnvironmentVariable("JWT__KEY")
               ?? configuration["Jwt:Key"]
               ?? throw new ArgumentNullException("JWT-ключ не задан ни в переменных окружения, ни в appsettings.json");

            if (Encoding.UTF8.GetBytes(_key).Length < 32)
                throw new ArgumentException("JWT Key должен быть не менее 32 байт.");
            _expiryMinutes = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? "60");
        }

        public string GenerateToken(Guid personId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, personId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
