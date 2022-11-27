using Payroll.Domain.Data.Repositories;
using Payroll.Domain.Shared.Models;
using Payroll.Domain.Shared.Utils;
using System;
using System.Threading.Tasks;

namespace Payroll.Domain.Business.Services
{

    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        Task<User> GetUserByIdAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserRepository _userRepository;

        public UserService(
            IJwtUtils jwtUtils,
            IUserRepository userRepository)
        {
            _jwtUtils = jwtUtils;
            _userRepository = userRepository;  
        }


        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
