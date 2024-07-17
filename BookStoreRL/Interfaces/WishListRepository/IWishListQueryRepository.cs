using BookStoreRL.Entity;
using BookStoreRL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.WishListRepository
{
    public interface IWishListQueryRepository
    {
        Task<IEnumerable<WishlistBookModel>> GetBooksFromWishListAsync(int userId);
    }
}
