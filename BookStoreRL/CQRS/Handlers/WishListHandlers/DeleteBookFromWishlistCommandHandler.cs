using BookStoreRL.Commands;
using BookStoreRL.Interfaces.WishListRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.Handlers
{
    public class DeleteBookFromWishlistCommandHandler : IRequestHandler<DeleteBookFromWishlistCommand>
    {
        private readonly IWishListCommandRepository _wishListCommandRepository;

        public DeleteBookFromWishlistCommandHandler(IWishListCommandRepository wishListCommandRepository)
        {
            _wishListCommandRepository = wishListCommandRepository;
        }

        public async Task<Unit> Handle(DeleteBookFromWishlistCommand request, CancellationToken cancellationToken)
        {
            await _wishListCommandRepository.DeleteBookFromWishListAsync(request.UserId, request.BookId);
            return Unit.Value;
        }
    }
}
