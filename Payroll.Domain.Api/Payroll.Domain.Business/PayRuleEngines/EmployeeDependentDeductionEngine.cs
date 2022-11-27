using Payroll.Domain.Shared.Enums;
using System;
using System.Linq;

namespace Payroll.Domain.Business.PayRuleEngines
{
    public class EmployeeDependentDeductionEngine : IPayRuleEngine
    {
        private IPayrollContext _context;
        public PayrollEngineSortType Sort => PayrollEngineSortType.EmployeeDependentDeductionEngine;
        public IPayrollContext Execute(IPayrollContext payrollContext)
        {
            _context = payrollContext;
            /*
                iterate only over the employee pay period deductions list as depedent deductions can not apply without 
                employee deductions based on enrollment
             */
            foreach (var employeeDeduction in _context.EmployeePayPeriodsDeductions) {
                
                //check if depedent exist for the employee
                var dependents = _context.EmployeeDependents.Where(x => x.EmployeeId == employeeDeduction.EmployeeId);
                if (dependents.Count() == 0) continue;

                var employeeBenefitPlan = _context.EmployeeBenefits
                                                    .First(x => x.EmployeeId == employeeDeduction.EmployeeId);
                var benefitPlanInfo = _context.BenefitPlans.FirstOrDefault(x => x.Id == employeeBenefitPlan.BenefitPlanId);
                if (benefitPlanInfo == null) continue;

                
                //apply dependent deduction only if configured on benefit plan
                if (benefitPlanInfo.DependentBenefitDeductionAmount > 0) {
                    foreach (var dependent in dependents)
                    {
                        var annualDeductionPerDependent = benefitPlanInfo.DependentBenefitDeductionAmount;
                        var hasDiscount = dependent.FirstName.StartsWith("A", StringComparison.OrdinalIgnoreCase);

                        if (hasDiscount) {
                            var applicableBenefitDeductionAfterDiscount = (100 - _context.ApplicableDiscount) / 100;
                            annualDeductionPerDependent *= applicableBenefitDeductionAfterDiscount;
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
