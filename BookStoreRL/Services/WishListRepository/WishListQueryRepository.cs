using BookStoreRL.CustomExceptions;
using BookStoreRL.Models;
using BookStoreRL.Interfaces.WishListRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreRL.Services.WishListRepository
{
    public class WishListQueryRepository : IWishListQueryRepository
    {
        private readonly UserDbContext _context;

        public WishListQueryRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishlistBookModel>> GetBooksFromWishListAsync(int userId)
        {
            try
            {
                var wishlistItems = await _context.GetWishlistItemsByUserIdAsync(userId);

                if (wishlistItems == null || !wishlistItems.Any())
                {
                    throw new WishlistException("No items found in the wishlist for the given user.");
                }

                var bookIds = string.Join(",", wishlistItems.Select(w => w.BookId));
                var books = await _context.GetBooksByIdsAsync(bookIds);

                var result = wishlistItems.Select(w => new WishlistBookModel
                {
                    BookId = w.BookId,
                    Title = books.FirstOrDefault(b => b.Id == w.BookId)?.Title ?? string.Empty,
                    Author = books.FirstOrDefault(b => b.Id == w.BookId)?.Author ?? string.Empty,
                    Price = books.FirstOrDefault(b => b.Id == w.BookId)?.Price ?? 0,
                    QuantityToPurchase = w.Quantity
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new WishlistException("An error occurred while retrieving wishlist items.", ex);
            }
        }

    }
}
