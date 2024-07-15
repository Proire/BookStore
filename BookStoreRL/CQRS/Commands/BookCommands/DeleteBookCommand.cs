using BookStoreRL.Entity;
using MediatR;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public int BookId { get; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }
    }
}
