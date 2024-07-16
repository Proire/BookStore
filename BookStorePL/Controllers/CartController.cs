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

    }
}
