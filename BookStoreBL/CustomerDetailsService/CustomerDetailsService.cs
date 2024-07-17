using BookStoreML;
using BookStoreRL.Commands;
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

        public async Task AddOrUpdateCustomerDetailsAsync(CustomerDetailsModel details,int userId)
        {
            EditCustomerDetailsCommand command = new EditCustomerDetailsCommand(details.AddressType, details.FullAddress, details.City, details.Country, details.Zipcode, details.State, userId);
            await _mediator.Send(command);
        }
    }
}
