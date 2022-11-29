using System.Net;
using System.Text.Json;

namespace Payroll.Domain.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            ArgumentNullException.ThrowIfNull(next, nameof(next));

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = exception switch
                {
                    UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
                    KeyNotFoundException _ => (int)HttpStatusCode.NotFound,// not found error
                    _ => (int)HttpStatusCode.InternalServerError,// all other exceptions
                };
                var result = JsonSerializer.Serialize(new { message = exception?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}