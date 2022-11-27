using Payroll.Domain.Shared.Enums;

namespace Payroll.Domain.Business
{
    public interface IPayRuleEngine
    {
        PayrollEngineSortType Sort { get; }
        IPayrollContext Execute(IPayrollContext payrollContext);
    }
}
