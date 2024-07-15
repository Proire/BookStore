using BookStoreML;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreRL.Interfaces
{
    public interface IUserCommandRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<User> LoginUserAsync(LoginModel model);
    }
}
