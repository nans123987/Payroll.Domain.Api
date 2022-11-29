using Payroll.Domain.Shared.Enums;

namespace Payroll.Domain.Shared.Entities
{
    public class Dependent
    {
        public Guid DependentId { get; set; }
        public int ClientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DependentRelationships Relationship { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
