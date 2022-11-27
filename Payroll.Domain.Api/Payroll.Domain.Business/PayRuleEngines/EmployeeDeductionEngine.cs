using Payroll.Domain.Shared.DTO;
using Payroll.Domain.Shared.Enums;
using System;
using System.Linq;

namespace Payroll.Domain.Business.PayRuleEngines
{
    public class EmployeeDeductionEngine : IPayRuleEngine
    {
        private IPayrollContext _context;
        public PayrollEngineSortType Sort => PayrollEngineSortType.EmployeeDeductionEngine;

        public IPayrollContext Execute(IPayrollContext payrollContext)
        {
            _context = payrollContext;

            foreach (var employee in _context.Employees) {
                //get employee benefit enrollment
                var employeeBenefit = _context.EmployeeBenefits.FirstOrDefault(x => x.EmployeeId == employee.Id);
                if (employeeBenefit == null) continue;
                
                //if benefit plan is not found or has been deleted enrollment is invalid, so continue with other EEs
                var benefitPlan = _context.BenefitPlans.FirstOrDefault(x => x.Id == employeeBenefit.BenefitPlanId);
                if (benefitPlan == null) continue;

                //add a deduction object only if the employee is enrolled in a benefit plan          
                var employeeAnnualDeductionAmount = benefitPlan.EmployeeBenefitDeductionAmount;

                if (employeeAnnualDeductionAmount > 0) {
                    //apply discount of 10% if the first name starts with A
                    var hasDiscount = employee.FirstName.StartsWith("a", StringComparison.OrdinalIgnoreCase);

                    //if discount needs to be applied then calculate the percentage applicable for the deduction
                    if (hasDiscount) {
                        var applicableBenefitDeductionAfterDiscount = (100 - _context.ApplicableDiscount) / 100;
                        employeeAnnualDeductionAmount *= applicableBenefitDeductionAfterDiscount;
                    }

                    var employeeDeductionAmount = decimal.Round(employeeAnnualDeductionAmount / _context.NoOfPayPeriods, 2);

                    //employee deduction object with calculation for employee deduction amount and set net pay after deduction
                    _context.EmployeePayPeriodsDeductions.Add(
                            new EmployeeDeduction(employee)
                            {
                                EmployeeDeductionAmount = employeeDeductionAmount,
                                TotalPayPeriodDeduction = employeeDeductionAmount,
                                NetPay = employee.BasePay - employeeDeductionAmount
                            }
                        );
                }
            }

            return _context;
        }
    }
}
