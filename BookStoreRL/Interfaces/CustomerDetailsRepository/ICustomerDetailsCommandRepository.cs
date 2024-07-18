using BookStoreRL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreRL.Entity;

namespace BookStoreRL.Interfaces.CustomerDetailsRepository
{
    public interface ICustomerDetailsCommandRepository
    {
        Task AddCustomerDetailsAsync(CustomerDetails customerDetails);

        Task UpdateCustomerDetailsAsync(CustomerDetails customerDetails);
    }
}
