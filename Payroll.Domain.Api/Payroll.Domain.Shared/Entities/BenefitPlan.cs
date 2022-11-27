using System;

namespace Payroll.Domain.Shared.Entities
{
    public class BenefitPlan
    {
        public Guid Id { get; set; }
        public Guid BenefitPlanProviderId { get; set; }
        public int ClientId { get; set; }
        public string BenefitPlanName { get; set; }
        public string BenefitDeductionCode { get; set; }
        public decimal EmployeeBenefitDeductionAmount { get; set; }
        public decimal DependentBenefitDeductionAmount { get; set; }
    }
}
