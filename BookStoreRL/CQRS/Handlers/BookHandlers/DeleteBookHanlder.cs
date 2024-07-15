using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookCommandRepository _bookCommandRepository;

        public DeleteBookCommandHandler(IBookCommandRepository bookCommandRepository)
        {
            _bookCommandRepository = bookCommandRepository;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            // Call DeleteAsync to handle the deletion logic and get the deleted book
            return await _bookCommandRepository.DeleteAsync(request.BookId);
        }
    }
}
