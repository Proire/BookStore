using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IBookCommandRepository _bookCommandRepository;

        public UpdateBookCommandHandler(IBookCommandRepository bookCommandRepository)
        {
            _bookCommandRepository = bookCommandRepository;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = request.BookId,
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                Publisher = request.Publisher,
                PublishedDate = request.PublishedDate,
                Genre = request.Genre,
                Language = request.Language,
                Pages = request.Pages,
                Price = request.Price,
                Description = request.Description,
                CoverImageUrl = request.CoverImageUrl,
                StockQuantity = request.StockQuantity,
                Rating = request.Rating
            };

            return await _bookCommandRepository.UpdateAsync(book);
        }
    }
}
