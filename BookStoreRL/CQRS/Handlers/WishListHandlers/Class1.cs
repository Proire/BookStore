using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookStoreRL.Models;
using BookStoreRL.Services.WishListRepository;
using BookStoreRL.Interfaces.WishListRepository;
using BookStoreRL.Queries;

namespace BookStoreRL.CQRS.Handlers.WishListHandlers
{
    public class GetBooksFromWishlistHandler : IRequestHandler<GetBooksFromWishlistQuery, IEnumerable<WishlistBookModel>>
    {
        private readonly IWishListQueryRepository _wishListQueryRepository;

        public GetBooksFromWishlistHandler(IWishListQueryRepository wishListQueryRepository)
        {
            _wishListQueryRepository = wishListQueryRepository;
        }

        public async Task<IEnumerable<WishlistBookModel>> Handle(GetBooksFromWishlistQuery request, CancellationToken cancellationToken)
        {
            return await _wishListQueryRepository.GetBooksFromWishListAsync(request.UserId);
        }
    }
}
