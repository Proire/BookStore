using BookStoreML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.UserRepository
{
    public interface IUserQueryRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllAsync();
    }

}
