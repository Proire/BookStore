using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.OrderRepository;
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
                // Call the DbContext method to execute the stored procedure
                await _context.AddOrderAsync(order);

                // Optionally, you can add further processing or validation here

                // Save changes to the DbContext (if needed)
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                throw new OrderException("An error occurred while adding the order.", ex);
            }
        }
    }
}
