using Dapper;
using Payroll.Domain.Shared.Models;
using System.Data;

namespace Payroll.Domain.Data.Repositories
{
    public interface IUserRepository {

        Task<User> GetUserByCredentialsAsync(string userName);
        Task<User> GetUserByIdAsync(int id);
    }
    public class UserRepository: SecurityDapperConnection,IUserRepository
    {
        public UserRepository(IDbConnectionFactory dbConnectionFactory): base(dbConnectionFactory)
        {

        }
        public async Task<User> GetUserByCredentialsAsync(string userName)
        {
            var sql = @"SELECT TOP(1) UserId, FirstName, LastName, Role, Username, PasswordHash FROM dbo.Users WHERE Username = @userName";
            using var connection = DbConnection;
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { userName }, commandType: CommandType.Text);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var sql = @"Select TOP(1) UserId, FirstName, LastName, Role, Username, PasswordHash FROM Users where UserId = @id";
            using var connection = DbConnection;
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { id }, commandType: CommandType.Text);

        }
    }
}
