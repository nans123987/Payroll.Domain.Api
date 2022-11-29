namespace Payroll.Domain.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseSecureApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.AuthenticateAsync(model);
            return Ok(response);
        }
    }
}
