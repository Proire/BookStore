using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces.CardRepository; // Corrected namespace for the repository interface
using BookStoreRL.CQRS.Commands.CartCommands;

namespace BookStoreRL.Handlers
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand>
    {
        private readonly ICartCommandRepository _commandRepository;

        public DeleteCartItemCommandHandler(ICartCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<Unit> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            await _commandRepository.DeleteBookFromCartAsync(request.UserId, request.BookId);
            return Unit.Value;
        }
    }
}
