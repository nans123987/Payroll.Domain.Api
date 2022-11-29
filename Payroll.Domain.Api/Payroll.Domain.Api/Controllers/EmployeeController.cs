namespace Payroll.Domain.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseSecureApiController
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
