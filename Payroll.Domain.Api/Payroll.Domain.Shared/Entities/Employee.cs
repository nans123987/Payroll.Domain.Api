namespace Payroll.Domain.Shared.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
        //defaulting the base pay to 2000.00
        public static decimal BasePay => (decimal)2000.00;
    }
}
