using BookStoreML;
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
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly UserDbContext _userDbContext;

        public OrderQueryRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext; 
        }

        public async Task<ICollection<Entity.Order>> GetOrders(int userId)
        {
            try
            {
                var orders = await _userDbContext.Orders.Include(x => x.CustomerDetail).Where(x => x.CustomerDetail.UserId == userId).ToListAsync();
                if(orders.Count == 0)
                {
                    throw new OrderException("No order Placed");
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw new UserException("An unexpected error occurred.", ex);
            }
        }
    }
}
