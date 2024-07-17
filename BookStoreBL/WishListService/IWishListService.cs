using BookStoreRL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.WishListService
{
    public interface IWishListService
    {
        Task AddBookToWishListAsync(int userid, int bookid, int quantity);

        Task DeleteBookFromWishListAsync(int userid, int bookid);

        Task<IEnumerable<WishlistBookModel>> GetBooksFromWishListAsync(int userId);
    }
}
