using BookStoreRL.Commands;
using BookStoreRL.CQRS.Commands.OrderCommand;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.OrderRepository;
using BookStoreRL.Interfaces.WishListRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Handlers.OrderHandler
{
    internal class OrderCommandHandler : IRequestHandler<OrderCommand>
    {
        private readonly IOrderCommandRepository _wishListCommandRepository;

        public OrderCommandHandler(IOrderCommandRepository wishListCommandRepository)
        {
            _wishListCommandRepository = wishListCommandRepository;
        }

        public async Task<Unit> Handle(OrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order() { CartId = request.CartId, TotalCartPrice = request.TotalCartPrice, CustomerDetailId = request.CustomerDetailsId };
            await _wishListCommandRepository.AddOrderAsync(order);
            return Unit.Value;
        }
    }
}
