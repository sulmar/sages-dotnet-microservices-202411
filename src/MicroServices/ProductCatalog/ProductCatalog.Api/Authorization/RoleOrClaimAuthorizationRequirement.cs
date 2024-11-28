using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace ProductCatalog.Api.Authorization;

public record RoleOrClaimAuthorizationRequirement(string role, string claimType) : IAuthorizationRequirement;

public class RoleOrClaimAuthorizationHandler : AuthorizationHandler<RoleOrClaimAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleOrClaimAuthorizationRequirement requirement)
    {
        var claimValue = context.User.FindFirstValue(requirement.claimType);

        var result = context.User.IsInRole(requirement.role) || !string.IsNullOrEmpty(claimValue);

        if (result)
            context.Succeed(requirement);        

        return Task.CompletedTask;

    }
}
