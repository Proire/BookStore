using BookStoreML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces
{
    public interface IUserQueryRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllAsync();
    }

}
