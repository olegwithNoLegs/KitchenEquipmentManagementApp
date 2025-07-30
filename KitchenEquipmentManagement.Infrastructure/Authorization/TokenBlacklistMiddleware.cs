using KitchenEquipmentManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Infrastructure.Authorization
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService)
        {
            var jti = context.User?.FindFirst("jti")?.Value;

            if (!string.IsNullOrEmpty(jti) && tokenService.IsRevoked(jti))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token has been revoked.");
                return;
            }

            await _next(context);
        }
    }
}
