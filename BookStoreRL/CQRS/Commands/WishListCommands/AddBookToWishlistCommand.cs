using MediatR;

namespace BookStoreRL.Commands
{
    public class AddBookToWishlistCommand : IRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public AddBookToWishlistCommand(int userId, int bookId, int quantity)
        {
            UserId = userId;
            BookId = bookId;
            Quantity = quantity;
        }
    }
}
