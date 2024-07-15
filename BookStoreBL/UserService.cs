﻿using BookStoreML;
using BookStoreRL.CQRS.Commands;
using BookStoreRL.CQRS.Commands.UserCommands;
using BookStoreRL.CQRS.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreBL
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateUserAsync(UserRegistrationModel user, string role)
        {
            var command = new InsertUserCommand(user.FullName, user.UserName, user.Email, user.Password, role, user.Phonenumber, user.DateOfBirth);
            await _mediator.Send(command);
        }

        public async Task<User> LoginUserAsync(LoginModel login)
        {
            var command = new LoginUserCommand(login.UserName, login.Password);
            return await _mediator.Send(command);
        }
    }
}
