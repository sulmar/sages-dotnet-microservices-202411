using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructures;

// dotnet add package System.IdentityModel.Tokens.Jwt

public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-your-secret-key-your-secret-key"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
