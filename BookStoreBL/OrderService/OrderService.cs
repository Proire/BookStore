using BookStoreML;
using BookStoreRL.CQRS.Commands.OrderCommand;
using BookStoreRL.CQRS.Queries.OrderQueries;
using BookStoreRL.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task AddOrderAsync(OrderModel order)
        {
            OrderCommand command = new OrderCommand(order.CartId, order.TotalCartPrice, order.CustomerDetailsId);
            await _mediator.Send(command);  
        }

        public async Task<ICollection<Order>> GetAllOrdersAsync(int userId)
        {
            GetOrdersQuery command = new GetOrdersQuery(userId);
            return await _mediator.Send(command);
        }
    }
}
