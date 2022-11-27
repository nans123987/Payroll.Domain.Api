using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Business.Services;
using Payroll.Domain.Shared.DTO;
using Payroll.Domain.Shared.Models;
using System;
using System.Threading.Tasks;
using AuthorizeAttribute = Payroll.Domain.Api.Authorization.AuthorizeAttribute;

namespace Payroll.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDeductionsController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public EmployeeDeductionsController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet]
        [Authorize(Roles.Admin)]
        [Route("client/{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeePayPeriodDeductions))]
        public async Task<IActionResult> GetEmployeeDeductions(int clientId)
        {
            var result = await _payrollService.GetEmployeesPayPeriodDeductionsAsync(clientId);
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles.Employee, Roles.Admin)]
        [Route("client/{clientId:int}/employee/{employeeId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeePayPeriodDeductions))]
        public async Task<IActionResult> GetEmployeeDeductions(int clientId, Guid employeeId)
        {
            var result = await _payrollService.GetEmployeesPayPeriodDeductionsAsync(clientId, employeeId);
            return Ok(result);
        }
    }
}
