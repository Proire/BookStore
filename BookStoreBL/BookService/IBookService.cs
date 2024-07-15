using BookStoreML;
using BookStoreRL.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBL
{
    public interface IBookService
    {
        Task CreateBookAsync(BookRegistrationModel book);
        Task<Book> UpdateBookAsync(BookUpdateModel book, int bookId);
        Task<Book> DeleteBookAsync(int bookId);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
    }
}
