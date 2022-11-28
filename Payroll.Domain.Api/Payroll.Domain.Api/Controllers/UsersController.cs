﻿using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Api.Authorization;
using Payroll.Domain.Business.Services;
using Payroll.Domain.Shared.Models;
using System.Threading.Tasks;
using AllowAnonymous = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace Payroll.Domain.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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