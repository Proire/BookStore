using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreRL.Models;
using BookStoreRL.CustomExceptions;
using BookStoreBL.CustomerDetailsService;
using UserModelLayer;
using BookStoreRL.Commands;
using BookStoreML;

namespace BookStorePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerDetailsService _customerDetailsService;

        public CustomerDetailsController(ICustomerDetailsService customerDetailsService)
        {
            _customerDetailsService = customerDetailsService;
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPut]
        [Route("edit")]
        public async Task<ResponseModel<string>> EditCustomerDetails([FromBody] CustomerDetailsModel editCustomerDetails)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _customerDetailsService.AddOrUpdateCustomerDetailsAsync(editCustomerDetails,userId);

                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Data = string.Empty,
                    Message = "Customer details updated successfully.",
                    Status = true
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
