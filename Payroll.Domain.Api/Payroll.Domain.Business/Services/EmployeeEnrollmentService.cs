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
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(employeeBenefitEnrollmentRepository, nameof(employeeBenefitEnrollmentRepository));

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
