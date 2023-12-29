using System.Security.Claims;

using GlobalCalc.Web.Services;

namespace GlobalCalc.Web.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authorizationService = context.RequestServices.GetRequiredService<AuthorizationService>();
            if (authorizationService.Validate(context, out string? authorizationMethod))
            {
                ClaimsIdentity identity = new(authorizationMethod, "Admin", "admin");
                ClaimsPrincipal principal = new(identity);
                context.User = principal;
            }

            await _next.Invoke(context);
        }
    }
}
