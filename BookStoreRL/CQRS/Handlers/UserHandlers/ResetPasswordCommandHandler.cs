using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.CQRS.Commands.UserCommands;
using BookStoreRL.Interfaces.UserRepository;
using BookStoreRL.Services.UserRepository;
using MediatR;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUserCommandRepository _userRepository;

    public ResetPasswordCommandHandler(IUserCommandRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {

        await _userRepository.ResetPassword(request.UserId, request.NewPassword);
        return Unit.Value;
    }
}
