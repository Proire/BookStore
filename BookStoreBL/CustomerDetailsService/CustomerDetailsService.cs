using BookStoreML;
using BookStoreRL.Commands;
using BookStoreRL.CQRS.Queries.CartQueries;
using BookStoreRL.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.CustomerDetailsService
{
    public class CustomerDetailsService : ICustomerDetailsService
    {
        private readonly IMediator _mediator;

        public CustomerDetailsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddCustomerDetailsAsync(AddCustomerDetailsCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<ICollection<CustomerDetail>> GetCustomerDetailsAsync(int userid)
        {
            GetCustomerDetailsQuery command = new GetCustomerDetailsQuery(userid);
            return await _mediator.Send(command);
        }

        public async Task UpdateCustomerDetailsAsync(UpdateCustomerDetailsCommand command)
        {
            await _mediator.Send(command);
        }

    }
}
