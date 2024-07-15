using BookStoreML;
using BookStoreRL.CQRS.Commands.UserCommands;
using BookStoreRL.Interfaces.UserRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Handlers.UserHandlers
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, User>
    {
        private readonly IUserCommandRepository _repository;

        public InsertUserCommandHandler(IUserCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role,
                Phonenumber = request.Phonenumber,
                DateOfBirth = request.DateOfBirth
            };
            Console.WriteLine(user.Id);

            await _repository.AddAsync(user);
            return user;
        }
    }
}
