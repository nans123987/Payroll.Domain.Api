using System.Data;
using System.Data.SqlClient;

namespace Payroll.Domain.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(Database connectionName);
    }
    public class DBConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<Database, string> _connectionsDictionary;

        public DBConnectionFactory(IDictionary<Database, string> connectionsDictionary)
        {
            ArgumentNullException.ThrowIfNull(connectionsDictionary, nameof(connectionsDictionary));

            _connectionsDictionary = connectionsDictionary;
        }

        public IDbConnection CreateDbConnection(Database connectionName)
        {
            if (_connectionsDictionary.TryGetValue(connectionName, out string? connectionString))
            {
                return new SqlConnection(connectionString);
            }
            throw new ArgumentNullException($"{connectionString}");
        }
    }
}
