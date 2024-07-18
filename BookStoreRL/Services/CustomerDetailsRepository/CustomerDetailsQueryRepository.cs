﻿using BookStoreRL.Entity;
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
    public class CustomerDetailsQueryRepository : ICustomerDetailsQueryRepostiory
    {
        private readonly UserDbContext _context;

        public CustomerDetailsQueryRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<CustomerDetails>> GetCustomerDetailsAsync(int userid)
        {
            try
            {
                return await _context.Set<CustomerDetails>()
                                      .Where(cd => cd.UserId == userid).ToListAsync<CustomerDetails>();
            }
            catch(Exception ex)
            {
                throw new CustomerDetailsException("An error occurred while fetching customer details.", ex);
            }
        }
    }
}