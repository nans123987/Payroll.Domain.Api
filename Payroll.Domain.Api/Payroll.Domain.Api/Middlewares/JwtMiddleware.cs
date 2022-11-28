using Microsoft.AspNetCore.Http;
using Payroll.Domain.Business.Services;
using Payroll.Domain.Shared.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Domain.Api.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetUserByIdAsync(userId.Value);
            }

            await _next(context);
        }
    }
}