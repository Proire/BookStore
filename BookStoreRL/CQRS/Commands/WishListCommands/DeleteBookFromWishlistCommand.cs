using MediatR;

namespace BookStoreRL.Commands
{
    public class DeleteBookFromWishlistCommand : IRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DeleteBookFromWishlistCommand(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
