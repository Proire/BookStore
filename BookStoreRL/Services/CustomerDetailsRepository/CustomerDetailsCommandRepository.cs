using BookStoreRL.Entity;
using BookStoreRL.Interfaces.CustomerDetailsRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Services.CustomerDetailsRepository
{
    public class CustomerDetailsCommandRepository : ICustomerDetailsCommandRepository
    {
        private readonly UserDbContext _context;

        public CustomerDetailsCommandRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateCustomerDetailsAsync(CustomerDetails customerDetails)
        {
            var existingCustomer = await _context.CustomerDetails
                .FirstOrDefaultAsync(c => c.Id == customerDetails.Id);

            if (existingCustomer != null)
            {
                // Update existing customer details
                existingCustomer.AddressType = customerDetails.AddressType;
                existingCustomer.FullAddress = customerDetails.FullAddress;
                existingCustomer.City = customerDetails.City;
                existingCustomer.Country = customerDetails.Country;
                existingCustomer.Zipcode = customerDetails.Zipcode;
                existingCustomer.State = customerDetails.State;

                _context.CustomerDetails.Update(existingCustomer);
            }
            else
            {
                // Add new customer details
                _context.CustomerDetails.Add(customerDetails);
            }

            await _context.SaveChangesAsync();
        }
    }

}
