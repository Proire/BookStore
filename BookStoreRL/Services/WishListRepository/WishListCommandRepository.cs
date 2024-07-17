using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces.WishListRepository;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStoreRL.Services.WishListRepository
{
    public class WishListCommandRepository : IWishListCommandRepository
    {
        private readonly UserDbContext _context;

        public WishListCommandRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddBooktoWishListAsync(int userId, int bookId, int quantity)
        {
            // Check if wishlist exists for the user
            var wishlist = await _context.Wishlists.Where(w => w.UserId == userId).ToListAsync();

            if (wishlist == null || wishlist.Count == 0)
            {
                // No wishlist found, create a new one
                var newWishlist = new Wishlist
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = quantity
                };

                _context.Wishlists.Add(newWishlist);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Wishlist found, check if the book is already in the wishlist
                var wishlistItem = wishlist.FirstOrDefault(w => w.BookId == bookId);

                if (wishlistItem != null)
                {
                    throw new WishlistException("The book is already in the wishlist.");
                }

                // Add the new book to the existing wishlist
                var newWishlistItem = new Wishlist
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = quantity
                };

                _context.Wishlists.Add(newWishlistItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookFromWishListAsync(int userId, int bookId)
        {
            // Check if wishlist exists for the user
            var wishlist = await _context.Wishlists
                                         .Where(w => w.UserId == userId && w.BookId == bookId)
                                         .FirstOrDefaultAsync();

            if (wishlist == null)
            {
                throw new WishlistException("The book is not in the wishlist.");
            }

            // Book is in the wishlist, delete it
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }
}
