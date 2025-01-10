using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth.Api.Infrastructures;

// dotnet add package System.IdentityModel.Tokens.Jwt

public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity)
    {
        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
            [JwtRegisteredClaimNames.Sub] = "public_key",
            [JwtRegisteredClaimNames.Email] = identity.Email,
            [JwtRegisteredClaimNames.Name] = identity.Email,
            [JwtRegisteredClaimNames.GivenName] = identity.FirstName,
            [JwtRegisteredClaimNames.FamilyName] = identity.LastName,
            [ClaimTypes.Role] = "trainer",
            [ClaimTypes.Role] = "developer"
        };
        
        string secretkey = "your-secret-key-your-secret-key-your-secret-key";
        
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretkey));

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = "your-issuer",
            Audience = "your-issuer",
            Claims = claims,
            IssuedAt = null,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var jwt_token = new JsonWebTokenHandler().CreateToken(descriptor);

        return jwt_token;
     
    }
}
