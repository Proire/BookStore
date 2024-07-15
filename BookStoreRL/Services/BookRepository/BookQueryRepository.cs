using BookStoreRL.Entity;
using BookStoreRL.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreRL.Interfaces.BookRepository;

namespace BookStoreRL.Services.BookRepository
{
    public class BookQueryRepository : IBookQueryRepository
    {
        private readonly UserDbContext _context;

        public BookQueryRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    throw new BookException($"No book found with id: {bookId}");
                }
                return book;
            }
            catch (Exception ex)
            {
                throw new BookException("An error occurred while retrieving the book.", ex);
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                if (books.Count == 0)
                {
                    throw new BookException("No books found.");
                }
                return books;
            }
            catch (Exception ex)
            {
                throw new BookException("An error occurred while retrieving the books.", ex);
            }
        }
    }
}
