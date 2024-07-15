using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.BookCommands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookCommandRepository _bookCommandRepository;

        public CreateBookCommandHandler(IBookCommandRepository bookCommandRepository)
        {
            _bookCommandRepository = bookCommandRepository;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
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

            await _bookCommandRepository.AddAsync(book);

            return book;
        }
    }
}
