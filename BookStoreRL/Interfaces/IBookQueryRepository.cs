using BookStoreRL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces
{
    public interface IBookQueryRepository
    {
        Task<Book> GetByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllAsync();
    }
}
