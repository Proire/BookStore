using BookStoreRL.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Commands.OrderCommand
{
    public class OrderCommand : IRequest
    {
        public int CartId { get; set; }

        public decimal TotalCartPrice { get; set; }

        public int CustomerDetailsId { get; set; }

        public OrderCommand(int cartid, decimal totalCartPrice,  int customerdetailsId) 
        { 
            this.CartId = cartid;
            this.TotalCartPrice = totalCartPrice;
            this.CustomerDetailsId = customerdetailsId;
        }
    }
}
