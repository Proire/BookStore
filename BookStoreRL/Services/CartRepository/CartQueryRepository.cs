using BookStoreRL.Entity;
using BookStoreRL.Interfaces.CartRepository;
using BookStoreRL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Services.CartRepository
{
    public class CartQueryRepository : ICartQueryRepository
    {
        private readonly UserDbContext _context;

        public CartQueryRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<CartSummaryModel> GetBooksFromCartAsync(int userId)
        {
            // Fetch the cart associated with the userId
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new CartSummaryModel
                {
                    Books = Enumerable.Empty<BookDetailModel>(),
                    TotalCartPrice = 0
                };
            }

            // Extract BookIds from the cart items
            var bookIds = cart.CartItems.Select(ci => ci.BookId).Distinct();

            // Fetch books corresponding to the extracted BookIds
            var books = await _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            // Create a dictionary to map BookId to QuantityToPurchase
            var bookIdToQuantity = cart.CartItems
                .GroupBy(ci => ci.BookId)
                .ToDictionary(g => g.Key, g => g.Sum(ci => ci.QuantityToPurchase));

            // Map books to BookDetailModel and calculate total price
            var bookDetails = books.Select(b => new BookDetailModel
            {
                BookId = b.Id,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price,
                QuantityToPurchase = bookIdToQuantity.ContainsKey(b.Id) ? bookIdToQuantity[b.Id] : 0,
                TotalPrice = bookIdToQuantity.ContainsKey(b.Id) ? b.Price * bookIdToQuantity[b.Id] : 0
            }).ToList();

            // Calculate the total price of the cart
            var totalCartPrice = bookDetails.Sum(bd => bd.TotalPrice);

            // Return the cart summary with book details and total cart price
            return new CartSummaryModel
            {
                Books = bookDetails,
                TotalCartPrice = totalCartPrice
            };
        }

    }
}
