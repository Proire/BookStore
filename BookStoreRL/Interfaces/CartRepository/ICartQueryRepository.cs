using BookStoreRL.Entity;
using BookStoreRL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.CartRepository
{
    public interface ICartQueryRepository
    {
        Task<CartSummaryModel> GetBooksFromCartAsync(int userid);
    }
}
