using BookStoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.CartService
{
    public interface ICartService
    {
        Task AddBookToCartAsync(int userid, int bookid, int quantity);
    }
}
