using BookStoreRL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.CustomerDetailsRepository
{
    public interface ICustomerDetailsQueryRepostiory
    {
        Task<ICollection<CustomerDetails>> GetCustomerDetailsAsync(int userid);
    }
}
