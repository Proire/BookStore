using BookStoreRL.CQRS.Queries.CartQueries;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.BookRepository;
using BookStoreRL.Interfaces.CartRepository;
using BookStoreRL.Interfaces.CustomerDetailsRepository;
using BookStoreRL.Services.CustomerDetailsRepository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries.BookQueries
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, ICollection<CustomerDetail>>
    {
        private readonly ICustomerDetailsQueryRepostiory _cartQueryRepository;

        public GetCustomerDetailsQueryHandler(ICustomerDetailsQueryRepostiory cartQueryRepository)
        {
            _cartQueryRepository = cartQueryRepository;
        }

        public async Task<ICollection<CustomerDetail>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _cartQueryRepository.GetCustomerDetailsAsync(request.UserId);
        }
    }
}
