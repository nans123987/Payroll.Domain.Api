using Microsoft.Extensions.Logging;
using Payroll.Domain.Data.Repositories;
using Payroll.Domain.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Payroll.Domain.Business.Services
{
    public interface IEmployeeEnrollmentService {
        Task CreateEmployeeBenefitEnrollmentAsync(EmployeeBenefitPlanEnrollmentRequest employeeBenefitPlanEnrollmentRequest);
    }
    public class EmployeeEnrollmentService : IEmployeeEnrollmentService
    {
        private readonly ILogger _logger; 
        private readonly IEmployeeRepository _employeeBenefitEnrollmentRepository;

        public EmployeeEnrollmentService(
            ILogger<EmployeeEnrollmentService> logger, 
            IEmployeeRepository employeeBenefitEnrollmentRepository)
        {
            _logger = logger;   
            _employeeBenefitEnrollmentRepository = employeeBenefitEnrollmentRepository;
        }
        public async Task CreateEmployeeBenefitEnrollmentAsync(
            EmployeeBenefitPlanEnrollmentRequest employeeBenefitPlanEnrollmentRequest)
        {
            try
            {
                await _employeeBenefitEnrollmentRepository.CreateEmployeeBenefitEnrollmentAsync(
                        employeeBenefitPlanEnrollmentRequest.ClientId,
                        employeeBenefitPlanEnrollmentRequest.EmployeeId,
                        employeeBenefitPlanEnrollmentRequest.BenefitPlanId);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"#ENROLLMENT ERROR: benefit enrollment failed 
                                    for 
                                    clientId:{employeeBenefitPlanEnrollmentRequest.ClientId} and 
                                    employeeId:{employeeBenefitPlanEnrollmentRequest.EmployeeId} and 
                                    benefitplanId:{employeeBenefitPlanEnrollmentRequest.BenefitPlanId}", ex);
                throw;
            }
                
        }
    }
}
