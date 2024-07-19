using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.OrderRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookStoreRL.Services.OrderRepository
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly UserDbContext _context;

        public OrderCommandRepository(UserDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                var cart = await _context.Carts.Where(x => x.Id == order.CartId).FirstOrDefaultAsync();
                if (cart != null)
                {
                    cart.IsPurchased = true;
                    var cartbooks = cart.CartItems.ToList();
                    var books = await _context.Books.ToListAsync();
                    foreach(var cartbook in cartbooks)
                    {
                        var book = books.Find(x => x.Id == cartbook.Id);
                        if(book != null)
                        {
                            book.StockQuantity -= cartbook.QuantityToPurchase; 
                        }
                    }
                    _context.Books.UpdateRange(books);
                    _context.Carts.Update(cart);
                    // Call the DbContext method to execute the stored procedure
                    await _context.AddOrderAsync(order);

                    // Save changes to the DbContext (if needed)
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                throw new OrderException("An error occurred while adding the order.", ex);
            }
        }
    }
}
