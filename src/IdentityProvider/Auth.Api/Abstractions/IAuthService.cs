using Auth.Api.Models;

namespace Auth.Api.Abstractions;

public interface IAuthService
{
    Task<AuthorizeResult> AuthorizeAsync(string username, string password);
}


public interface ITokenService
{
    string CreateAccessToken(UserIdentity identity);
}

public record AuthorizeResult(bool isAuthorized, UserIdentity identity);