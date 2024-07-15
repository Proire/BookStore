using BookStoreML;
using BookStoreRL.CQRS.Commands.UserCommands;
using MediatR;
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
