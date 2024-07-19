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
        private readonly EmailSender _emailSender;

        public UserCommandRepository(UserDbContext context, JwtTokenGenerator jwtTokenGenerator, EmailSender emailSender)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailSender = emailSender;
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

        public async Task<string> ForgetPassword(string email)
        {
            try
            {
                User? user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new UserException("Email Not Found. Register First");
                }
                string token = _jwtTokenGenerator.GenerateUserValidationToken(Convert.ToString(user.Id), TimeSpan.FromMinutes(15));
                _emailSender.SendEmail(new EmailDTO() { To = user.Email, Subject = "Reset Password", Body = token });
                return token;
            }
            catch (DbUpdateException ex)
            {           
                throw new UserException("An error occurred while adding the user to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new UserException("An unexpected error occurred.", ex);
            }
        }

        public async Task ResetPassword(int UserId, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == UserId);
                if (user != null)
                {
                    // Generate a unique key and IV for the user
                    using (Aes aes = Aes.Create())
                    {
                        aes.GenerateKey();
                        aes.GenerateIV();
                        byte[] key = aes.Key;
                        byte[] iv = aes.IV;

                        // Store the key and IV in a file
                        KeyIvManager.UpdateKeyAndIv(user.UserName, key, iv);

                        // Hash the user's password using the generated key and IV
                        user.Password = PasswordHasher.HashPassword(user.Password, key, iv);

                    }

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new UserException($"No User Found with id : {UserId}");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new UserException("An error occurred while adding the user to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new UserException("An unexpected error occurred.", ex);
            }
        }
    }
}
