using BookStoreRL.Commands;
using BookStoreRL.Interfaces.WishListRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.Handlers
{
    public class AddBookToWishlistCommandHandler : IRequestHandler<AddBookToWishlistCommand>
    {
        private readonly IWishListCommandRepository _wishListCommandRepository;

        public AddBookToWishlistCommandHandler(IWishListCommandRepository wishListCommandRepository)
        {
            _wishListCommandRepository = wishListCommandRepository;
        }

        public async Task<Unit> Handle(AddBookToWishlistCommand request, CancellationToken cancellationToken)
        {
            await _wishListCommandRepository.AddBooktoWishListAsync(request.UserId, request.BookId, request.Quantity);
            return Unit.Value;
        }
    }
}
