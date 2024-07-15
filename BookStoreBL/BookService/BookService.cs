using BookStoreML;
using BookStoreRL.CQRS.Commands.BookCommands;
using BookStoreRL.CQRS.Queries.BookQueries;
using BookStoreRL.Entity;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBL
{
    public class BookService : IBookService
    {
        private readonly IMediator _mediator;

        public BookService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateBookAsync(BookRegistrationModel book)
        {
            var command = new CreateBookCommand(
                book.Title,
                book.Author,
                book.ISBN,
                book.Publisher,
                book.PublishedDate,
                book.Genre,
                book.Language,
                book.Pages,
                book.Price,
                book.Description,
                book.CoverImageUrl,
                book.StockQuantity,
                book.Rating
            );
            await _mediator.Send(command);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var query = new GetBookByIdQuery(bookId);
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var query = new GetAllBooksQuery();
            return await _mediator.Send(query);
        }

        public async Task<Book> UpdateBookAsync(BookUpdateModel book, int bookId)
        {
            var command = new UpdateBookCommand(
                bookId,
                book.Title,
                book.Author,
                book.ISBN,
                book.Publisher,
                book.PublishedDate,
                book.Genre,
                book.Language,
                book.Pages,
                book.Price,
                book.Description,
                book.CoverImageUrl,
                book.StockQuantity,
                book.Rating
            );
            return await _mediator.Send(command);
        }

        public async Task<Book> DeleteBookAsync(int bookId)
        {
            var command = new DeleteBookCommand(bookId);
            return await _mediator.Send(command);
        }
    }
}
