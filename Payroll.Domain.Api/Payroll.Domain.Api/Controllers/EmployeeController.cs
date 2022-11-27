using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Api.Authorization;
using Payroll.Domain.Business.Services;
using Payroll.Domain.Shared.Models;
using System.Threading.Tasks;

namespace Payroll.Domain.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeEnrollmentService _employeeEnrollmentService;
        public EmployeeController(IEmployeeEnrollmentService employeeEnrollmentService)
        {
            _employeeEnrollmentService = employeeEnrollmentService; 
        }



        [Authorize(Roles.Admin, Roles.Employee)]
        [HttpPost("enrollment")]
        public async Task<IActionResult> CreateEmployeeBenefitPlanEnrollment(EmployeeBenefitPlanEnrollmentRequest request)
        {
            await _employeeEnrollmentService.CreateEmployeeBenefitEnrollmentAsync(request);
            return Ok();
        }
    }
}
