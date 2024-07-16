using BookStoreRL.Entity;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces.CardRepository
{
    public interface ICartCommandRepository
    {
        Task AddBooktoCartAsync(int userid, int bookid, int quantity);

        Task CartItemQuantityAsync(int userid, int bookid, int quantity);

        Task DeleteBookFromCartAsync(int userid,int bookid);
    }
}
