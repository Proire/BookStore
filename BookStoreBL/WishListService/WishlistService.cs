using System.Threading.Tasks;
using MediatR;
using BookStoreRL.Commands;
using BookStoreBL.WishListService;
using BookStoreRL.Models;
using BookStoreRL.CQRS.Queries.BookQueries;
using BookStoreRL.Queries;

namespace BookStoreRL.Services
{
    public class WishlistService : IWishListService
    {
        private readonly IMediator _mediator;

        public WishlistService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddBookToWishListAsync(int userId, int bookId, int quantity)
        {
            var command = new AddBookToWishlistCommand(userId, bookId, quantity);
            await _mediator.Send(command);
        }

        public async Task DeleteBookFromWishListAsync(int userId, int bookId)
        {
            var command = new DeleteBookFromWishlistCommand(userId, bookId);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<WishlistBookModel>> GetBooksFromWishListAsync(int userId)
        {
            var command = new GetBooksFromWishlistQuery(userId);
            return await _mediator.Send(command);
        }
    }
}
