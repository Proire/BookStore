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
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }

        public OrderCommand(int bookId, string bookTitle, int quantity, decimal totalPrice, int userId) 
        { 
            this.BookId = bookId;
            this.BookTitle = bookTitle;
            this.Quantity = quantity;
            this.TotalPrice = totalPrice;
            this.UserId = userId;
        }
    }
}
