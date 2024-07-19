using BookStoreRL.CustomExceptions;
using BookStoreRL.Entity;
using BookStoreRL.Exceptions;
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

        public async Task AddCustomerDetailsAsync(CustomerDetail customerDetails)
        {

            try
            {
                // Add new customer details
                _context.CustomerDetails.Add(customerDetails);
            }
            catch (Exception ex)
            {
                throw new CustomerDetailsException(ex.Message, ex);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerDetailsAsync(CustomerDetail customerDetails)
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
                throw new CustomerDetailsException($"Customer detail with Id : {customerDetails.Id} not found");
            }

            await _context.SaveChangesAsync();
        }
    }

}
