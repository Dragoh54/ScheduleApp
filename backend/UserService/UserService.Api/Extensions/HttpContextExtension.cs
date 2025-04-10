namespace UserService.Api.Extensions;

public static class HttpContextExtension
{
    public static string GetBearerToken(this HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers.Authorization.ToString();
        
        if (string.IsNullOrEmpty(authorizationHeader))
            throw new InvalidOperationException("Authorization header is missing");
        
        if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Authorization header must use Bearer scheme");

        return authorizationHeader.Replace("Bearer ", string.Empty);
    }
}