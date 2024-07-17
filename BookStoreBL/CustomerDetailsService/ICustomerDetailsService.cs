using BookStoreML;
using BookStoreRL.Commands;
using BookStoreRL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.CustomerDetailsService
{
    public interface ICustomerDetailsService
    {
        Task AddOrUpdateCustomerDetailsAsync(CustomerDetailsModel details, int userId);
    }
}
