using BookStoreRL.Entity;
using MediatR;
using System.Collections.Generic;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
    }
}
