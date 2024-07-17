using BookStoreML;
using BookStoreRL.CQRS.Commands.OrderCommand;
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
        public async Task AddOrderAsync(OrderModel order,int userId)
        {
            OrderCommand command = new OrderCommand(order.BookId, order.BookTitle, order.Quantity, order.TotalPrice, userId);
            await _mediator.Send(command);  
        }
    }
}
