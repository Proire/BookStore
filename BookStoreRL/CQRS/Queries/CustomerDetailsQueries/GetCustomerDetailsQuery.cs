using MediatR;
using BookStoreRL.Models;
using BookStoreRL.Entity;

namespace BookStoreRL.CQRS.Queries.CartQueries
{
    public class GetCustomerDetailsQuery : IRequest<ICollection<CustomerDetail>>
    {
        public int UserId { get; set; }

        public GetCustomerDetailsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
