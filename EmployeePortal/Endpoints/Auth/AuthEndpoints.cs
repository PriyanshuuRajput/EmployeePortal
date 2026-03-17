using Application.Dto.AuthDto;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace EmployeePortal.Endpoints.Auth
{
    public static  class AuthEndpoints
    {
        public static  void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/admin-login", async (HttpContext context, LoginDto loginDto, IAuthService authService) =>
            {
                var result = await authService.LoginAsync(loginDto);

                if (!result.Success)
                    return Results.Unauthorized();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.User.Username),
                    new Claim(ClaimTypes.Role, result.User.Role)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await context.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return Results.Ok();
            });

            app.MapPost("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Ok();
            }).DisableAntiforgery();
        }

    }
}
