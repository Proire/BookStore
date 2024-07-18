using System;
using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.CQRS.Commands.UserCommands;
using BookStoreRL.Interfaces.UserRepository;
using BookStoreRL.Services.UserRepository;
using MediatR;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, string>
{
    private readonly IUserCommandRepository _userRepository;

    public ForgetPasswordCommandHandler(IUserCommandRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.ForgetPassword(request.Email);
    }
}
