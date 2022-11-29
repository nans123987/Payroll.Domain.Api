using System.Data;

namespace Payroll.Domain.Data
{
    public abstract class SecurityDapperConnection
    {
        private IDbConnection? _dbConnection;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SecurityDapperConnection(IDbConnectionFactory dbConnectionFactory)
        {
            ArgumentNullException.ThrowIfNull(dbConnectionFactory, nameof(dbConnectionFactory));

            _dbConnectionFactory = dbConnectionFactory;
        }

        public IDbConnection DbConnection
        {
            get
            {
                if (!string.IsNullOrEmpty(_dbConnection?.ConnectionString)) return _dbConnection;
                _dbConnection = _dbConnectionFactory.CreateDbConnection(Database.SECURITY);
                return _dbConnection;
            }
            private set
            {
            }
        }

    }

    public abstract class PayrollDapperConnection
    {
        private IDbConnection? _dbConnection;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PayrollDapperConnection(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IDbConnection DbConnection
        {
            get
            {
                if (!string.IsNullOrEmpty(_dbConnection?.ConnectionString)) return _dbConnection;
                _dbConnection = _dbConnectionFactory.CreateDbConnection(Database.PAYROLL);
                return _dbConnection;
            }
            private set
            {
            }
        }

    }
}
