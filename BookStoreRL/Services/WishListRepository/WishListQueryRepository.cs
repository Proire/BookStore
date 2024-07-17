using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Models;
using BookStoreRL.Interfaces.WishListRepository;

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
            // Fetch the wishlist items for the specified user
            var wishlistItems = await _context.Wishlists
                .Where(w => w.UserId == userId)
                .ToListAsync();

            // Check if any wishlist items are found
            if (wishlistItems == null || wishlistItems.Count == 0)
            {
                throw new WishlistException("No items found in the wishlist for the given user.");
            }

            // Retrieve the book details for the found wishlist items
            var bookIds = wishlistItems.Select(w => w.BookId).ToList();
            var books = await _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            // Map the wishlist items and book details to WishlistBookModel
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
    }
}
