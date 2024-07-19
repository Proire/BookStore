using BookStoreRL.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Queries.OrderQueries
{
    public class GetOrdersQuery : IRequest<ICollection<Order>>  
    {
        public int UserId { get; set; }

        public GetOrdersQuery(int userId)
        {
            UserId = userId;
        }   
    }
}
