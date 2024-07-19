using BookStoreML;
using BookStoreRL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.OrderService
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderModel order);

        Task<ICollection<Order>> GetAllOrdersAsync(int userId);
    }
}
