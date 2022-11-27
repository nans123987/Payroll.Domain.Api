using Payroll.Domain.Shared.DTO;
using Payroll.Domain.Shared.Entities;
using System.Collections.Generic;

namespace Payroll.Domain.Business
{
    public interface IPayrollContext {
        int ClientId { get; set; }
        int NoOfPayPeriods { get;}
        decimal ApplicableDiscount { get; }
        List<Employee> Employees { get; set; }
        List<BenefitPlan> BenefitPlans { get; set; } 
        List<Dependent> EmployeeDependents { get; set; }
        List<EmployeeBenefit> EmployeeBenefits { get; set; }
        List<EmployeeDeduction> EmployeePayPeriodsDeductions { get; set; }
    }

    public class PayrollContext: IPayrollContext
    {
        public int ClientId { get; set; }

        //assuming applicable discount is 10%
        public decimal ApplicableDiscount => (decimal)10.00;

        //assuming number of pay periods is 26
        public int NoOfPayPeriods => 26;
        public List<Employee> Employees { get; set; }
        public List<BenefitPlan> BenefitPlans { get; set; }
        public List<Dependent> EmployeeDependents { get; set; }
        public List<EmployeeBenefit> EmployeeBenefits { get; set; }
        public List<EmployeeDeduction> EmployeePayPeriodsDeductions { get; set; }

        public PayrollContext()
        {
            Employees = new List<Employee>();
            BenefitPlans = new List<BenefitPlan>();
            EmployeeDependents = new List<Dependent>();
            EmployeeBenefits = new List<EmployeeBenefit>();
            EmployeePayPeriodsDeductions = new List<EmployeeDeduction>();
        }
    }
}
