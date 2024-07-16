using MediatR;

namespace BookStoreRL.CQRS.Commands.CartCommands
{
    public class DeleteCartItemCommand : IRequest
    {
        public int UserId { get; set; } // UserId is needed to find the cart
        public int BookId { get; set; }

        public DeleteCartItemCommand(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
