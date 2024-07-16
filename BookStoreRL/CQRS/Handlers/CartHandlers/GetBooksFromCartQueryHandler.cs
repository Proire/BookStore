using BookStoreRL.CQRS.Queries.CartQueries;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using BookStoreRL.Interfaces.CartRepository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetBooksFromCartQueryHandler : IRequestHandler<GetBooksFromCartQuery, CartSummaryModel>
    {
        private readonly ICartQueryRepository _cartQueryRepository;

        public GetBooksFromCartQueryHandler(ICartQueryRepository cartQueryRepository)
        {
            _cartQueryRepository = cartQueryRepository;
        }

        public async Task<CartSummaryModel> Handle(GetBooksFromCartQuery request, CancellationToken cancellationToken)
        {
            return await _cartQueryRepository.GetBooksFromCartAsync(request.UserId);
        }
    }
}
