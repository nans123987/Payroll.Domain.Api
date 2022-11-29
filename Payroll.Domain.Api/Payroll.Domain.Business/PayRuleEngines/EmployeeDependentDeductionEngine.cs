using Payroll.Domain.Business.RuleHelpers;
using Payroll.Domain.Shared.Enums;

namespace Payroll.Domain.Business.PayRuleEngines
{
    public class EmployeeDependentDeductionEngine : IPayRuleEngine
    {
        private IPayrollContext? _context;
        public PayrollEngineSortType Sort => PayrollEngineSortType.EmployeeDependentDeductionEngine;
        public IPayrollContext Execute(IPayrollContext payrollContext)
        {
            ArgumentNullException.ThrowIfNull(payrollContext, nameof(payrollContext));

            _context = payrollContext;
            /*
                iterate only over the employee pay period deductions list as depedent deductions can not apply without 
                employee deductions based on enrollment
             */
            foreach (var employeeDeduction in _context.EmployeePayPeriodsDeductions) {
                
                //check if depedent exist for the employee
                var dependents = _context.EmployeeDependents.Where(x => x.EmployeeId == employeeDeduction.EmployeeId);
                if (!dependents.Any()) continue;

                var employeeBenefitPlan = _context.EmployeeBenefits
                                                    .First(x => x.EmployeeId == employeeDeduction.EmployeeId);
                var benefitPlanInfo = _context.BenefitPlans.FirstOrDefault(x => x.Id == employeeBenefitPlan.BenefitPlanId);
                if (benefitPlanInfo == null) continue;

                
                //apply dependent deduction only if configured on benefit plan
                if (benefitPlanInfo.DependentBenefitDeductionAmount > 0) {
                    foreach (var dependent in dependents)
                    {
                        var annualDeductionPerDependent = benefitPlanInfo.DependentBenefitDeductionAmount;
                        var hasDiscount = dependent.FirstName?.StartsWith("a", StringComparison.OrdinalIgnoreCase) ?? false;

                        //if discount needs to be applied then calculate the percentage applicable for the deduction
                        if (hasDiscount)
                        {
                            annualDeductionPerDependent = annualDeductionPerDependent
                                                        .ComputeDeductionDiscount(_context.ApplicableDiscount);
                        }

                        var dependentDeductionAmount = decimal.Round(annualDeductionPerDependent / _context.NoOfPayPeriods, 2);

                        //add dependent deductions in total and dependent deduction amount
                        employeeDeduction.DependentDeductionAmount += dependentDeductionAmount;
                        employeeDeduction.TotalPayPeriodDeduction += dependentDeductionAmount;
                        employeeDeduction.NetPay -= dependentDeductionAmount;
                    }
                }
            }

            return _context;
        }
    }
}
