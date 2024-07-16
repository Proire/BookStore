using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Services;
using BookStoreRL.CQRS.Commands.CartCommands;
using BookStoreRL.Interfaces.CardRepository;

namespace BookStoreRL.Handlers
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand>
    {
        private readonly ICartCommandRepository _commandRepository;

        public UpdateQuantityCommandHandler(ICartCommandRepository cardCommandRepository)
        {
            _commandRepository = cardCommandRepository;
        }

        public async Task<Unit> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            await _commandRepository.CartItemQuantityAsync(request.UserId, request.BookId, request.Quantity);
            return Unit.Value;
        }
    }
}
