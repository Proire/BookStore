using BookStoreBL;
using BookStoreML;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreRL.Entity;
using UserModelLayer;
using Microsoft.AspNetCore.Authorization;

namespace BookStorePL.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPost]
        [Route("/createBook")]
        public async Task<ResponseModel<string>> CreateBook([FromBody] BookRegistrationModel bookModel)
        {
            try
            {
                // Set the Role manually here if needed
                await _bookService.CreateBookAsync(bookModel);
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "Book Added Successfully",
                    Data = "Go to GetAllBooks",
                    Status = true
                };
                return responseModel;
            }
            catch (BookException ex)
            {
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPut("{bookId}")]
        public async Task<ResponseModel<Book>> UpdateBook([FromBody] BookUpdateModel bookModel, int bookId)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(bookModel, bookId);
                return new ResponseModel<Book>
                {
                    Data = updatedBook,
                    Message = "Book updated successfully",
                    Status = true
                };
            }
            catch (Exception ex) // Handle specific exceptions as needed
            {
                return new ResponseModel<Book>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpDelete("{bookId}")]
        public async Task<ResponseModel<Book>> DeleteBook(int bookId)
        {
            try
            {
                var deletedBook = await _bookService.DeleteBookAsync(bookId);
                return new ResponseModel<Book>
                {
                    Data = deletedBook,
                    Message = "Book deleted successfully",
                    Status = true
                };
            }
            catch (Exception ex) // Handle specific exceptions as needed
            {
                return new ResponseModel<Book>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("{bookId}")]
        public async Task<ResponseModel<Book>> GetBookById(int bookId)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return new ResponseModel<Book>
                    {
                        Data = null,
                        Message = $"No book found with id: {bookId}",
                        Status = false
                    };
                }
                return new ResponseModel<Book>
                {
                    Data = book,
                    Message = "Book retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Book>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet]
        public async Task<ResponseModel<IEnumerable<Book>>> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return new ResponseModel<IEnumerable<Book>>
                {
                    Data = books,
                    Message = "Books retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Book>>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
