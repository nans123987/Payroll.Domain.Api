using Payroll.Domain.Business;
using Payroll.Domain.Business.Configuration;
using Payroll.Domain.Business.PayRuleEngines;

namespace Payroll.Domain.Api.Configuration
{
    public static class DependencyRegistry
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration) 
        {
            //context
            services.AddScoped<IPayrollContext, PayrollContext>();

            //engines
            services.AddScoped<IPayRuleEngine, EmployeeDeductionEngine>();
            services.AddScoped<IPayRuleEngine, EmployeeDependentDeductionEngine>();

            //utisl and services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeEnrollmentService, EmployeeEnrollmentService>();
            services.AddScoped<IPayrollService, PayrollService>();

            //repositories
            services.ConfigureDataServices(configuration);
        }
    }
}
