using System.Security.Claims;

namespace BrainFlow.UI.Web.Middleware
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Adicionar informações do usuário ao ViewBag se autenticado
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userInfo = new
                {
                    Id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    Nome = context.User.FindFirst("NomeCompleto")?.Value,
                    Email = context.User.FindFirst(ClaimTypes.Email)?.Value,
                    Tipo = context.User.FindFirst("TipoUsuario")?.Value,
                    IsAuthenticated = true
                };

                context.Items["UserInfo"] = userInfo;
            }
            else
            {
                context.Items["UserInfo"] = new { IsAuthenticated = false };
            }

            await _next(context);
        }
    }

    public static class UserInfoMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserInfoMiddleware>();
        }
    }
}