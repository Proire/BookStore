using BookStoreRL.CQRS.Queries.OrderQueries;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.OrderRepository;
using BookStoreRL.Services.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Handlers.OrderHandler
{
    public class GetOrderCommandHandler : IRequestHandler<GetOrdersQuery,ICollection<Order>>
    {
        private readonly IOrderQueryRepository _orderQueryRepository;

        public GetOrderCommandHandler(IOrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository;
        }

        public Task<ICollection<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _orderQueryRepository.GetOrders(request.UserId);
        }
    }
}
