using BookStoreRL.Entity;
using BookStoreRL.CustomExceptions;
using BookStoreRL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookStoreRL.Services
{
    public class BookCommandRepository : IBookCommandRepository
    {
        private readonly UserDbContext _context;

        public BookCommandRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new BookException("An error occurred while adding the book to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new BookException("An unexpected error occurred.", ex);
            }
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            try
            {
                var existingBook = await _context.Books.FindAsync(book.Id);
                if (existingBook == null)
                {
                    throw new BookException($"No book found with id: {book.Id}");
                }

                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.Publisher = book.Publisher;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.Genre = book.Genre;
                existingBook.Language = book.Language;
                existingBook.Pages = book.Pages;
                existingBook.Price = book.Price;
                existingBook.Description = book.Description;
                existingBook.CoverImageUrl = book.CoverImageUrl;
                existingBook.StockQuantity = book.StockQuantity;
                existingBook.Rating = book.Rating;

                _context.Books.Update(existingBook);
                await _context.SaveChangesAsync();

                return existingBook;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new BookException("A concurrency conflict occurred while updating the book.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new BookException("An error occurred while updating the book in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new BookException("An unexpected error occurred.", ex);
            }
        }

        public async Task<Book> DeleteAsync(int bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                    return book;
                }
                else
                {
                    throw new BookException($"No book found with id: {bookId}");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                throw new BookException("An error occurred while deleting the book from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new BookException("An unexpected error occurred.", ex);
            }
        }
    }
}
