using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MenuDigital.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        return int.Parse(value!);
    }
}