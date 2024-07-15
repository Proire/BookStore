using BookStoreML;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces.UserRepository;
using BookStoreRL.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UserModelLayer;

namespace BookStoreRL.Services.UserRepository
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly UserDbContext _context;

        public UserCommandRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            // Generate a unique key and IV for the user
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                // Store the key and IV in a file
                KeyIvManager.SaveKeyAndIv(user.UserName, key, iv);

                // Hash the user's password using the generated key and IV
                user.Password = PasswordHasher.HashPassword(user.Password, key, iv);
            }

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new UserException("An error occurred while adding the user to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new UserException("An unexpected error occurred.", ex);
            }
        }




        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new UserException("A concurrency conflict occurred while updating the user.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new UserException("An error occurred while updating the user in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new UserException("An unexpected error occurred.", ex);
            }
        }

        public async Task<User> LoginUserAsync(LoginModel model)
        {
            // Check if the user exists asynchronously
            bool isUser = await _context.Users.AnyAsync(x => x.UserName == model.UserName);

            if (isUser)
            {
                // Retrieve the user asynchronously
                User? user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);

                if (user != null)
                {
                    // Retrieve the key and IV for the user
                    (byte[] key, byte[] iv) = KeyIvManager.GetKeyAndIv(model.UserName);

                    // Verify the password
                    byte[] cipheredPassword = Convert.FromBase64String(user.Password);
                    string decryptedPassword = PasswordHasher.VerifyPassword(cipheredPassword, key, iv);

                    if (model.Password == decryptedPassword)
                    {
                        return user;
                    }

                    throw new UserException("Wrong Password, Reenter Password");
                }

                throw new UserException("Invalid UserName, Register First");
            }

            throw new UserException("Invalid UserName, Register First");
        }


        public async Task DeleteAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new UserException($"No user found with id: {userId}");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new UserException("An error occurred while deleting the user from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new UserException("An unexpected error occurred.", ex);
            }
        }
    }
}
