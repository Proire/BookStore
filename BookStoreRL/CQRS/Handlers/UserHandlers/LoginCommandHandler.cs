using BookStoreML;
using BookStoreRL.CQRS.Commands.UserCommands;
using BookStoreRL.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreRL.CQRS.Handlers.UserHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginUserCommand, User>
    {
        private readonly IUserCommandRepository _repository;

        public LoginCommandHandler(IUserCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var login = new LoginModel(request.UserName, request.Password);
            var user = await _repository.LoginUserAsync(login);
            return user;
        }
    }
}
