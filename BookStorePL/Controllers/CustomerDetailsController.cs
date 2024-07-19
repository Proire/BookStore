using BookStoreBL.CustomerDetailsService;
using BookStoreRL.Commands;
using BookStoreRL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerDetailsService _customerDetailsService;

        public CustomerDetailsController(ICustomerDetailsService customerDetailsService)
        {
            _customerDetailsService = customerDetailsService;
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPost]
        [Route("/customerdetail/add")]
        public async Task<IActionResult> AddCustomerDetails([FromBody] AddCustomerDetailsCommand addCustomerDetails)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            addCustomerDetails.UserId = userId;

            try
            {
                await _customerDetailsService.AddCustomerDetailsAsync(addCustomerDetails);

                return Ok(new ResponseModel<string>
                {
                    Data = string.Empty,
                    Message = "Customer details added successfully.",
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                });
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpPut]
        [Route("/customerdetail/edit")]
        public async Task<IActionResult> EditCustomerDetails([FromBody] UpdateCustomerDetailsCommand editCustomerDetails)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            editCustomerDetails.UserId = userId;

            try
            {
                await _customerDetailsService.UpdateCustomerDetailsAsync(editCustomerDetails);

                return Ok(new ResponseModel<string>
                {
                    Data = string.Empty,
                    Message = "Customer details updated successfully.",
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                });
            }
        }

        [Authorize(AuthenticationSchemes = "UserScheme", Roles = "User")]
        [HttpGet]
        [Route("/customerdetail/customerdetails")]
        public async Task<IActionResult> GetCustomerDetails()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            try
            {
                ICollection<CustomerDetail> customerDetails = await _customerDetailsService.GetCustomerDetailsAsync(userId);

                return Ok(new ResponseModel<ICollection<CustomerDetail>>
                {
                    Data = customerDetails,
                    Message = "Customer details retrieved successfully.",
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Message = "An unexpected error occurred.",
                    Status = false
                });
            }
        }
    }
}
