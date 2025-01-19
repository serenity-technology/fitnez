using System.Security.Claims;

namespace Share;

public static class ClaimsPrincipalExtension
{
    public static Guid UserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims.SingleOrDefault(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (claim != null)
        {
            var userId = new Guid(claim.Value);
            return userId;
        }
        else
            return Guid.Empty;
    }
}