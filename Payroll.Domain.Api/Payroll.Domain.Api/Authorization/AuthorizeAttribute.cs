using Microsoft.AspNetCore.Mvc.Filters;

namespace Payroll.Domain.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Roles[] _roles;

        public AuthorizeAttribute(params Roles[] roles)
        {
            _roles = roles ?? Array.Empty<Roles>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymous>().Any();
            if (allowAnonymous)
                return;

            // check if user exists in httpContext
            var user = (User?)context.HttpContext.Items["User"];
            if (user == null)
                //no token provided
                context.Result = new JsonResult(new { message = "Not Authenticated" }) { StatusCode = StatusCodes.Status401Unauthorized };

            else if (_roles.Any() && !_roles.Contains(user.Role))
            {
                //not authorized
                context.Result = new JsonResult(new { message = "You do not have enough privileges to access this resource" }) 
                { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
