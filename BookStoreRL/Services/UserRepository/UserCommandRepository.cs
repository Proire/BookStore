using BookStoreML;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces.UserRepository;
using BookStoreRL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using UserModelLayer;
using UserRLL.Utilities;

namespace BookStoreRL.Services.UserRepository
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly UserDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserCommandRepository(UserDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
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

        public async Task<string> LoginUserAsync(LoginModel model)
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
                        string token = string.Empty;
                        if (user.Role == "User")
                            token = _jwtTokenGenerator.GenerateUserToken(Convert.ToString(user.Id), user.UserName, TimeSpan.FromMinutes(15));
                        else
                            token = _jwtTokenGenerator.GenerateAdminToken(Convert.ToString(user.Id), user.UserName, TimeSpan.FromMinutes(15));

                        return token;
                    }

                    throw new UserException("Wrong Password, Reenter Password");
                }

                throw new UserException("Invalid UserName, Register First");
            }

            throw new UserException("Invalid UserName, Register First");
        }
    }
}
