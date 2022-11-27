using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Domain.Data;
using Payroll.Domain.Data.Repositories;
using System.Collections.Generic;

namespace Payroll.Domain.Business.Configuration
{
    public static class RepositoryConfiguration
    {
        public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration) {

            var connectionStringsConfiguration = configuration.GetSection("ConnectionStrings");

            var connectionDictionary = new Dictionary<Database, string>
            {
                { Database.PAYROLL, connectionStringsConfiguration.GetSection("PayrollDB").Value },
                { Database.SECURITY, connectionStringsConfiguration.GetSection("SecurityDB").Value }
            };

            //configure connections
            services.AddSingleton<IDictionary<Database, string>>(connectionDictionary);
            
            //connectionfactory injection
            services.AddTransient<IDbConnectionFactory, DBConnectionFactory>();

            //repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IBenefitPlanRepository, BenefitPlanRepository>();
        }


    }
}
