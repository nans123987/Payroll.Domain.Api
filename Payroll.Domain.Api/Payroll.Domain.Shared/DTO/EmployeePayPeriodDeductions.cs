namespace Payroll.Domain.Shared.DTO
{
    public class EmployeePayPeriodDeductions
    {
        public EmployeePayPeriodDeductions()
        {
            EmployeeDeductions = new List<EmployeeDeduction>();
        }
        public int ClientId { get; set; }

        public List<EmployeeDeduction> EmployeeDeductions { get; set; }
    }
}
