using Payroll.Domain.Shared.Entities;

namespace Payroll.Domain.Shared.DTO
{
    public class EmployeeDeduction
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal EmployeeDeductionAmount { get; set; }
        public decimal DependentDeductionAmount { get; set; }
        public decimal TotalPayPeriodDeduction { get; set; }
        public decimal NetPay { get; set; }

        public EmployeeDeduction()
        {

        }

        public EmployeeDeduction(Employee employee)
        {
            EmployeeId = employee.Id;
            EmployeeName = $"{employee.LastName},{employee.FirstName}";
            EmployeeDeductionAmount = (decimal)0.00;
            DependentDeductionAmount = (decimal)0.00;
            TotalPayPeriodDeduction = (decimal)0.00;
        }
    }
}
