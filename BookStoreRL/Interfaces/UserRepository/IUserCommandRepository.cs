﻿using BookStoreML;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreRL.Interfaces.UserRepository
{
    public interface IUserCommandRepository
    {
        Task AddAsync(User user);
        Task<string> LoginUserAsync(LoginModel model);

        Task<string> ForgetPassword(string email);

        Task ResetPassword(int UserId, string password);
    }
}
