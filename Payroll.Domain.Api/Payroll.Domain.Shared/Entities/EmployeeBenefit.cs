using System;

namespace Payroll.Domain.Shared.Entities
{
    public class EmployeeBenefit
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid BenefitPlanId { get; set; }
    }
}
