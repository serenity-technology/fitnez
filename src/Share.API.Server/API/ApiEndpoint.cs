using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Share;

public abstract class ApiEndpoint
{
    protected static IResult Ok<T>(T result) => TypedResults.Ok(result);

    protected static string UserName(ClaimsPrincipal user)
    {
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        return email ?? "anonymous";
    }
}