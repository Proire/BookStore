using BookStoreRL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.OrderRepository
{
    public interface IOrderQueryRepository
    {
        Task<ICollection<Order>> GetOrders(int userId);
    }
}
