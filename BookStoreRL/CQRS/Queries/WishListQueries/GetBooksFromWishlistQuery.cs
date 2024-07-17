using MediatR;
using System.Collections.Generic;
using BookStoreRL.Models;

namespace BookStoreRL.Queries
{
    public class GetBooksFromWishlistQuery : IRequest<IEnumerable<WishlistBookModel>>
    {
        public int UserId { get; set; }

        public GetBooksFromWishlistQuery(int userId)
        {
            UserId = userId;
        }
    }
}
