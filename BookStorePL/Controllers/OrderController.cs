using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStoreRL.Entity;
using BookStoreRL.Services;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using BookStoreBL.OrderService;
using UserModelLayer;
using BookStoreML;

namespace BookStorePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPost]
        [Route("add")]
        public async Task<ResponseModel<string>> AddOrder([FromBody] OrderModel order)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _orderService.AddOrderAsync(order,userId);

                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Data = string.Empty,
                    Message = "Order added successfully.",
                    Status = true
                };
                return responseModel;
            }
            catch (OrderException ex)
            {
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                };
                return responseModel;
            }
        }
    }
}
