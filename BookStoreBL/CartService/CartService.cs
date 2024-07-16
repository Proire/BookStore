using BookStoreRL.CQRS.Commands.CartCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStoreBL.CartService
{
    public class CartService : ICartService
    {

        private readonly IMediator _mediator;

        public CartService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task AddBookToCartAsync( int userid, int bookid, int quantity)
        {
            AddCartItemCommand command = new AddCartItemCommand(userid, bookid, quantity);
            await _mediator.Send(command);
        }

        public async Task UpdateQuantityAsync(int userid, int bookid, int quantity)
        {
            UpdateQuantityCommand command = new UpdateQuantityCommand(userid, bookid,quantity);
            await _mediator.Send(command);
        }
    }
}
