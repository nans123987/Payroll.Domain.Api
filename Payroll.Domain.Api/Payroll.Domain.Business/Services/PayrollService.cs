using Dapper;
using Microsoft.Extensions.Logging;
using Payroll.Domain.Data.Repositories;
using Payroll.Domain.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Domain.Business.Services
{
    public interface IPayrollService {
        Task<EmployeePayPeriodDeductions> GetEmployeesPayPeriodDeductionsAsync(int clientId, Guid? employeeId = null);
    }
    public class PayrollService : IPayrollService
    {
        private readonly ILogger _logger;
        private readonly IPayrollContext _payrollContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBenefitPlanRepository _benefitPlanRepository;
        private readonly IEnumerable<IPayRuleEngine> _engines;
        public PayrollService(
            ILogger<PayrollService> logger, 
            IPayrollContext payrollContext,
            IEmployeeRepository employeeRepository,
            IBenefitPlanRepository benefitPlanRepository,
            IEnumerable<IPayRuleEngine> engines)
        {
            _logger = logger;
            _payrollContext = payrollContext;
            _employeeRepository = employeeRepository;
            _benefitPlanRepository = benefitPlanRepository;
            _engines = engines;
        }

        public async Task<EmployeePayPeriodDeductions> GetEmployeesPayPeriodDeductionsAsync(int clientId, Guid? employeeId = null)
        {
            try
            {
                if (_engines == null || clientId == 0)
                    throw new ArgumentNullException(@$"#PayrollService: Failed to initialize data for clientId:{clientId}");


                await InitializePayrollContext(clientId, employeeId);

                //sort engines based on sort order
                var engines = _engines.OrderBy(x => x.Sort);

                //process payroll context
                foreach (var payRuleEngine in engines)
                {
                    payRuleEngine.Execute(_payrollContext);
                }

                //aggregate results
                var employeePayPeriodDeductions = GetEmployeePayperiodDeductions(clientId);

                return employeePayPeriodDeductions;
            }
            catch (Exception ex)
            {
                _logger.LogError($@"#PayrollService: Failed to process pay period data for clientId:{clientId}", ex);
                throw;
            }
        }

        private EmployeePayPeriodDeductions GetEmployeePayperiodDeductions(int clientId)
        {
            var employeePayPeriodDeductions = new EmployeePayPeriodDeductions() { ClientId = clientId };

            foreach (var employee in _payrollContext.Employees)
            {
                var employeeDeduction = _payrollContext.EmployeePayPeriodsDeductions
                                                            .FirstOrDefault(x => x.EmployeeId == employee.Id);
                if (employeeDeduction != null)
                    employeePayPeriodDeductions.EmployeeDeductions.Add(employeeDeduction);
                else
                    employeePayPeriodDeductions.EmployeeDeductions.Add(
                            new EmployeeDeduction(employee)
                            {
                                EmployeeId = employee.Id,
                                EmployeeName = $"{employee.LastName},{employee.FirstName}",
                                NetPay = employee.BasePay
                            }
                        );

            }

            return employeePayPeriodDeductions;
        }

        private async Task InitializePayrollContext(int clientId, Guid? employeeId)
        {
            _payrollContext.ClientId = clientId;
            if (employeeId.HasValue)
                //get only the request employee
                _payrollContext.Employees.AddRange(
                    await _employeeRepository.GetEmployeeAsync(clientId, employeeId.Value)
                    );

            else
                //get all employees by clientId
                _payrollContext.Employees.AddRange(
                    await _employeeRepository.GetEmployeesByClientIdAsync(clientId)
                    );

            var employeeIds = _payrollContext.Employees.Select(employee => employee.Id);

            if (employeeIds.Count() == 0)
                throw new ArgumentNullException(@$"#PayrollService: No Employees returned for clientId: {clientId}");

            employeeIds = employeeIds.Distinct();

            //read master data and populate the context
            var employeeDependents = await _employeeRepository
                                            .GetEmployeeDependentsAsync(clientId, employeeIds.AsList());
            var employeeBenefits = await _employeeRepository.
                                            GetEmployeeBenefitsAsync(clientId, employeeIds.AsList());

            var benefitPlans = await _benefitPlanRepository.GetBenefitPlansAsync(clientId);


            _payrollContext.EmployeeDependents.AddRange(employeeDependents);
            _payrollContext.EmployeeBenefits.AddRange(employeeBenefits);
            _payrollContext.BenefitPlans.AddRange(benefitPlans);
        }
    }
}
