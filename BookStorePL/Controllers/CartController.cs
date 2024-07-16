using BookStoreBL;
using BookStoreBL.CartService;
using BookStoreML;
using BookStoreRL.CustomExceptions.BookStoreRL.Exceptions;
using BookStoreRL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserModelLayer;

namespace BookStorePL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPost]
        [Route("/addtocart")]
        public async Task<ResponseModel<Cart>> AddToCart([FromBody] AddCartItemModel addCartItemModel)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _cartService.AddBookToCartAsync(userId, addCartItemModel.BookId, addCartItemModel.Quantity);

                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "Book added to cart successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (CartException ex)
            {
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPut]
        [Route("/updatequantity/{bookId}")]
        public async Task<ResponseModel<Cart>> UpdateQuantityAsync(int bookId, [FromBody] UpdateQuantityModel updateQuantityModel)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _cartService.UpdateQuantityAsync(userId, bookId, updateQuantityModel.Quantity);

                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "Book quantity updated successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (CartException ex)
            {
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpGet]
        [Route("books")]
        public async Task<ResponseModel<CartSummaryModel>> GetBooksFromCartAsync()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                // Call the CartService method to get the cart summary
                var result = await _cartService.GetBooksFromCartAsync(userId);

                if (result == null)
                {
                    return new ResponseModel<CartSummaryModel>
                    {
                        Message = "No cart found for the user.",
                        Status = false
                    };
                }

                // Return the response model with success status
                return new ResponseModel<CartSummaryModel>
                {
                    Data = result,
                    Message = "Cart retrieved successfully.",
                    Status = true
                };
            }
            catch (CartException ex)
            {
                // Handle specific cart exceptions
                return new ResponseModel<CartSummaryModel>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                return new ResponseModel<CartSummaryModel>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpDelete]
        [Route("/deletebook/{bookId}")]
        public async Task<ResponseModel<Cart>> DeleteBookFromCartAsync(int bookId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                // Call the service method to delete the book from the cart
                await _cartService.DeleteBookFromCartAsync(userId, bookId);

                // Prepare and return the response model
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "Book removed from cart successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (CartException ex)
            {
                // Handle specific cart exceptions
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                ResponseModel<Cart> responseModel = new ResponseModel<Cart>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }
    }
}
