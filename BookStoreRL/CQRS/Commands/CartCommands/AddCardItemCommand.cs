using MediatR;

namespace BookStoreRL.CQRS.Commands.CartCommands
{
    public class AddCartItemCommand : IRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public AddCartItemCommand(int userId, int bookId, int quantity)
        {
            UserId = userId;
            BookId = bookId;
            Quantity = quantity;
        }
    }
}
