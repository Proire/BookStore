using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookQueryRepository _bookQueryRepository;

        public GetAllBooksQueryHandler(IBookQueryRepository bookQueryRepository)
        {
            _bookQueryRepository = bookQueryRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookQueryRepository.GetAllAsync();
        }
    }
}
