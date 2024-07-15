using BookStoreML;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreRL.Services
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly UserDbContext _context;

        public UserQueryRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new UserException($"No user found with id: {userId}");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new UserException("An error occurred while retrieving the user.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                if (users.Count == 0)
                {
                    throw new UserException("No users found.");
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new UserException("An error occurred while retrieving the users.", ex);
            }
        }
    }

}
