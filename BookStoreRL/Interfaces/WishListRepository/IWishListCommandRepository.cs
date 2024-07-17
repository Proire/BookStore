using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.WishListRepository
{
    public interface IWishListCommandRepository
    {
        Task AddBooktoWishListAsync(int userid, int bookid, int quantity);
        Task DeleteBookFromWishListAsync(int userId, int bookid);

    }
}
