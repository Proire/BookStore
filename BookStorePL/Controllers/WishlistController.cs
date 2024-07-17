using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreRL.Services;
using BookStoreRL.CustomExceptions;
using BookStore.Models;
using BookStoreRL.Models;
using UserModelLayer;
using BookStoreBL.WishListService;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishListService _wishlistService;

        public WishlistController(IWishListService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPost]
        [Route("addtowishlist")]
        public async Task<ResponseModel<Wishlist>> AddBookToWishlist([FromBody] AddWishlistItemModel addWishlistItemModel)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _wishlistService.AddBookToWishListAsync(userId, addWishlistItemModel.BookId, addWishlistItemModel.Quantity);

                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = "Book added to wishlist successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (WishlistException ex)
            {
                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpDelete]
        [Route("deletefromwishlist/{bookId}")]
        public async Task<ResponseModel<Wishlist>> DeleteBookFromWishlist(int bookId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _wishlistService.DeleteBookFromWishListAsync(userId, bookId);

                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = "Book removed from wishlist successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (WishlistException ex)
            {
                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Wishlist> responseModel = new ResponseModel<Wishlist>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpGet("{userId}")]
        public async Task<ResponseModel<IEnumerable<WishlistBookModel>>> GetBooksFromWishlistAsync(int userId)
        {
            try
            {
                // Call the WishListService method to get the wishlist summary
                var result = await _wishlistService.GetBooksFromWishListAsync(userId);

                if (result == null || !result.Any())
                {
                    return new ResponseModel<IEnumerable<WishlistBookModel>>
                    {
                        Message = "No items found in the wishlist for the user.",
                        Status = false
                    };
                }

                // Return the response model with success status
                return new ResponseModel<IEnumerable<WishlistBookModel>>
                {
                    Data = result,
                    Message = "Wishlist retrieved successfully.",
                    Status = true
                };
            }
            catch (WishlistException ex)
            {
                // Handle specific wishlist exceptions
                return new ResponseModel<IEnumerable<WishlistBookModel>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                return new ResponseModel<IEnumerable<WishlistBookModel>>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
            }
        }
    }
}
