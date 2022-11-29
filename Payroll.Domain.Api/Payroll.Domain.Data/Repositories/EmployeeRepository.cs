using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payroll.Domain.Shared.Entities;
using System.Data;

namespace Payroll.Domain.Data.Repositories
{
    public interface IEmployeeRepository {
        Task CreateEmployeeBenefitEnrollmentAsync(int clientId, Guid employeeId, Guid benefitPlanId);
        Task<List<Employee>> GetEmployeeAsync(int clientId, Guid employeeId);
        Task<List<Employee>> GetEmployeesByClientIdAsync(int clientId);
        Task<List<Dependent>> GetEmployeeDependentsAsync(int clientId, List<Guid> employeeIds);
        Task<List<EmployeeBenefit>> GetEmployeeBenefitsAsync(int clientId, List<Guid> employeeIds);
    }

    public class EmployeeRepository : PayrollDapperConnection, IEmployeeRepository
    {
        private readonly ILogger _logger;
        public EmployeeRepository(
            ILogger<EmployeeRepository> logger,
            IDbConnectionFactory connectionFactory): base(connectionFactory)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(connectionFactory, nameof(connectionFactory));

            _logger = logger;
        }
        public async Task CreateEmployeeBenefitEnrollmentAsync(int clientId, Guid employeeId, Guid benefitPlanId)
        {
            try
            {
                var commandSql = @"INSERT INTO dbo.EmployeeBenefits
                                (
                                    Id,
                                    ClientId,
                                    Employee,
                                    BenefitPlan
                                )
                                VALUES (@id, @clientId, @employeeId, @benefitPlanId)";

                using var connection = DbConnection;
                await connection.ExecuteAsync(commandSql,
                    new
                    {
                        id = Guid.NewGuid(),
                        clientId,
                        employeeId,
                        benefitPlanId
                    }, commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                _logger.LogError($@"ENROLLMENT DATA ERROR: failed to create enrollment for 
                                clientId:{clientId} 
                                and employeeId:{employeeId}
                                benefitplanId:{benefitPlanId}", ex);
                throw;
            }

        }

        public async Task<List<Employee>> GetEmployeeAsync(int clientId, Guid employeeId)
        {
            try
            {
                var sql = @"SELECT TOP (1) 
	                               [Id]
                                  ,[EmployeeId]
                                  ,[FirstName]
                                  ,[LastName]
                                  ,[ClientId]
                                  ,[Active] AS IsActive
                              FROM [Payroll].[dbo].[Employee]
                              wHERE [ClientId] = @clientId AND Id = @employeeId and [Active] = 1";

                using var connection = DbConnection;
                var employees = await connection.QueryAsync<Employee>(sql, new
                {
                    clientId,
                    employeeId
                }, commandType: CommandType.Text);

                return employees.AsList();

            }
            catch (Exception ex)
            {
                _logger.LogError($@"#EmployeeRepository: Failed to read employee with 
                                    clientId: {clientId} and EmployeeId:{employeeId}", ex);
                throw;
            }
        }

        public async Task<List<Employee>> GetEmployeesByClientIdAsync(int clientId)
        {
            try
            {
                var sql = @"SELECT [Id]
                                  ,[EmployeeId]
                                  ,[FirstName]
                                  ,[LastName]
                                  ,[ClientId]
                                  ,[Active] AS IsActive
                              FROM [Payroll].[dbo].[Employee]
                              wHERE [ClientId] = @clientId and [Active] = 1";

                using var connection = DbConnection;
                var employees = await connection.QueryAsync<Employee>(sql, new
                {
                    clientId
                }, commandType: CommandType.Text);

                return employees.AsList();

            }
            catch (Exception ex)
            {
                _logger.LogError($@"#EmployeeRepository: Failed to read employee with 
                                    clientId: {clientId}", ex);
                throw;
            }
        }

        public async Task<List<EmployeeBenefit>> GetEmployeeBenefitsAsync(int clientId, List<Guid> employeeIds)
        {
            try
            {
                var sql = @"SELECT [Id]
                                  ,[ClientId]
                                  ,[Employee] AS EmployeeId
                                  ,[BenefitPlan] AS BenefitPlanId
                              FROM [Payroll].[dbo].[EmployeeBenefits]
                              WHERE ClientId = @clientId AND Employee IN @employeeIds";

                using var connection = DbConnection;
                var employeeBenefits = await connection.QueryAsync<EmployeeBenefit>(
                        sql,
                        new
                        {
                            clientId,
                            employeeIds
                        });
                return employeeBenefits.AsList();
            }
            catch (Exception ex)
            {
                _logger.LogError($@"#EmployeeRepository: Failed to read employee benefits with 
                                    clientId: {clientId} and employeeIds:{JsonConvert.SerializeObject(employeeIds)}", ex);
                throw;
            }
        }

        public async Task<List<Dependent>> GetEmployeeDependentsAsync(int clientId, List<Guid> employeeIds)
        {
            try
            {
                var sql = @"SELECT [DependentId]
	                              ,[ClientId]
                                  ,[FirstName]
                                  ,[LastName]
	                              ,[Relationship]
                                  ,[Employee] AS EmployeeId
                          FROM [Payroll].[dbo].[Dependents]
                          WHERE [ClientId] = @clientId AND Employee IN @employeeIds";

                using var connection = DbConnection;
                var dependents = await connection.QueryAsync<Dependent>(sql,
                                                    new
                                                    {
                                                        clientId,
                                                        employeeIds
                                                    });
                return dependents.AsList();
            }
            catch (Exception ex)
            {
                _logger.LogError($@"#EmployeeRepository: Failed to read employee dependents with 
                                    clientId: {clientId} and employeeIds:{JsonConvert.SerializeObject(employeeIds)}", ex);
                throw;
            }

        }
    }
}
