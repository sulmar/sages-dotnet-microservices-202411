using Auth.Api.Abstractions;
using Auth.Api.Models;
using System.Net.WebSockets;

namespace Auth.Api.Infrastructures;

public class FakeTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity)
    {
        return "abc";
    }
}
