using BookStoreML;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreBL
{
    public interface IUserService
    {
        Task CreateUserAsync(UserRegistrationModel user, string role);

        Task<string> LoginUserAsync(LoginModel login);

        Task<string> ForgetPasswordAsync(string email);

        Task ResetPassword(int userId, string password);
    }
}
