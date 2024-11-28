using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructures;

public class AuthService(IPasswordHasher<UserIdentity> passwordHasher) : IAuthService
{
    public Task<AuthorizeResult> AuthorizeAsync(string username, string password)
    {
        // TODO: Utwórz repo z metodą GetByUsername()
        var identity = new UserIdentity
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john@domain.com"
        };
        identity.HashedPassword = passwordHasher.HashPassword(identity, "123");

        var result = passwordHasher.VerifyHashedPassword(identity, identity.HashedPassword, password);

        if (result == PasswordVerificationResult.Success)
        {
            return Task.FromResult(new AuthorizeResult(true, identity));
        }
        else
        {
            return Task.FromResult(new AuthorizeResult(false, identity));
        }
    }
}
