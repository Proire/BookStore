using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.OrderRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                // Add the order to the Orders DbSet
                await _context.Orders.AddAsync(order);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                throw new OrderException("An error occurred while updating the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                throw new OrderException("An unexpected error occurred.", ex);
            }
        }
    }
}
