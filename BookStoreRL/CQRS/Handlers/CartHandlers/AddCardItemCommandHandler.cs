using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Services;
using BookStoreRL.CQRS.Commands.CartCommands;
using BookStoreRL.Interfaces.CardRepository;

namespace BookStoreRL.Handlers
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly ICartCommandRepository _commandRepository;

        public AddCartItemCommandHandler(ICartCommandRepository cardCommandRepository)
        {
            _commandRepository = cardCommandRepository; 
        }

        public async Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            await _commandRepository.AddBooktoCartAsync(request.UserId, request.BookId, request.Quantity);
            return Unit.Value;
        }
    }
}
