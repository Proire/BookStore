using BookStoreRL.Commands;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.CustomerDetailsRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.CQRS.Handlers.CustomerDetailsHandler
{
    public class AddCustomerDetailsHandler : IRequestHandler<AddCustomerDetailsCommand>
    {
        private readonly ICustomerDetailsCommandRepository _repository;

        public AddCustomerDetailsHandler(ICustomerDetailsCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddCustomerDetailsCommand request, CancellationToken cancellationToken)
        {
            // Create a CustomerDetails object from the command
            var customerDetails = new CustomerDetail
            {
                AddressType = request.AddressType,
                FullAddress = request.FullAddress,
                City = request.City,
                Country = request.Country,
                Zipcode = request.Zipcode,
                State = request.State,
                UserId = request.UserId,
            };

            // Pass the CustomerDetails object to the repository method
            await _repository.AddCustomerDetailsAsync(customerDetails);
            return Unit.Value;
        }
    }
}
