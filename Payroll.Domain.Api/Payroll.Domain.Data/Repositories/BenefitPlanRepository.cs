using Dapper;
using Microsoft.Extensions.Logging;
using Payroll.Domain.Shared.Entities;

namespace Payroll.Domain.Data.Repositories
{
    public interface IBenefitPlanRepository { 
        Task<List<BenefitPlan>> GetBenefitPlansAsync(int clientId);
    }
    public class BenefitPlanRepository: PayrollDapperConnection, IBenefitPlanRepository
    {
        private readonly ILogger _logger;
        public BenefitPlanRepository(ILogger<BenefitPlanRepository> logger, IDbConnectionFactory dbConnectionFactory): 
            base(dbConnectionFactory)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(dbConnectionFactory, nameof(dbConnectionFactory));

            _logger = logger; 
        }

        public async Task<List<BenefitPlan>> GetBenefitPlansAsync(int clientId)
        {
            try
            {
                var sql = @"SELECT [Id]
                                  ,[BenefitPlanProviderUid]
                                  ,[BenefitPlanName]
                                  ,[BenefitDeductionCode]
                                  ,[EmployeeBenefitDeductionAmount]
                                  ,[DependentBenefitDeductionAmount]
                                  ,[ClientId]
                          FROM [Payroll].[dbo].[BenefitPlan]
                          WHERE ClientId = @clientId";

                using var connection = DbConnection;
                var benefitPlans = await connection.QueryAsync<BenefitPlan>(sql,
                    new
                    {
                        clientId
                    });

                return benefitPlans.AsList();
            }
            catch (Exception ex)
            {
                _logger.LogError($@"#BenefitPlanRepository: Failed to retrieve the benefit plans for client id:{clientId}", ex);
                throw;
            }
        }
    }
}
