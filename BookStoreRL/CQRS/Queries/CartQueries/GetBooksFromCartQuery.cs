using MediatR;
using BookStoreRL.Models;

namespace BookStoreRL.CQRS.Queries.CartQueries
{
    public class GetBooksFromCartQuery : IRequest<CartSummaryModel>
    {
        public int UserId { get; set; }

        public GetBooksFromCartQuery(int userId)
        {
            UserId = userId;
        }
    }
}
