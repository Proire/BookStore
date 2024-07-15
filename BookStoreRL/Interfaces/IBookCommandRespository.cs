using BookStoreRL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Interfaces
{

    public interface IBookCommandRepository
    {
        Task AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<Book> DeleteAsync(int bookId);
    }

}
