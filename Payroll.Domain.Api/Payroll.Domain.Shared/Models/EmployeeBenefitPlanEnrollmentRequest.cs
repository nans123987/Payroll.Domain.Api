namespace Payroll.Domain.Shared.Models
{
    public class EmployeeBenefitPlanEnrollmentRequest
    {
        public int ClientId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid BenefitPlanId { get; set; }
    }
}
