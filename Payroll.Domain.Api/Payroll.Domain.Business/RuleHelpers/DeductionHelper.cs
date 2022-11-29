namespace Payroll.Domain.Business.RuleHelpers
{
    public static class DeductionHelper
    {
        public static decimal ComputeDeductionDiscount(this decimal amount, decimal applicableDiscount)
        {
            var applicableBenefitDeductionAfterDiscount = (100 - applicableDiscount) / 100;
            amount *= applicableBenefitDeductionAfterDiscount;

            return amount;
        }
    }
}
