using BookStoreRL.Entity;
using MediatR;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int BookId { get; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
