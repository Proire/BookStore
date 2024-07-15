using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookQueryRepository _bookQueryRepository;

        public GetBookByIdQueryHandler(IBookQueryRepository bookQueryRepository)
        {
            _bookQueryRepository = bookQueryRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookQueryRepository.GetByIdAsync(request.BookId);
        }
    }
}
