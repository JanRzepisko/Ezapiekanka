using Microsoft.AspNetCore.Authorization;

namespace ezapiekanka.JwtService;

public class JwtPolicies
{
    public const string User = "User";
    public const string Vendor = "Vendor";

    public static AuthorizationPolicy Policy(string role)
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(role).Build();
    }
}