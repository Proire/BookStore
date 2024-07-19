using BookStoreBL;
using BookStoreML;
using BookStoreRL;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserModelLayer;

namespace BookStorePL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/user/register")]
        public async Task<ResponseModel<User>> CreateUser([FromBody] UserRegistrationModel user)
        {
            try
            {
                await _userService.CreateUserAsync(user,"User");
                ResponseModel<User> responseModel = new ResponseModel<User>()
                {
                    Message = "User Added Successfully, Go to Login"
                };
                return responseModel;
            }
            catch (UserException ex)
            {
                ResponseModel<User> responseModel = new ResponseModel<User>()
                {
                    Message = ex.Message,
                    Status = false
                };
                return responseModel;
            }
        }

        [HttpPost]
        [Route("/user/login")]
        public async Task<ResponseModel<string>> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                // Call the asynchronous Login method from the business logic layer (BLL)
                string token = await _userService.LoginUserAsync(model);

                // Prepare the response model with user data
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "Logged In Successfully!",
                    Data = token, // Return the user object
                    Status = true
                };

                return responseModel;
            }
            catch (UserException ex)
            {
                // Handle the exception and prepare the response model
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Data = "Try Again", 
                    Status = false
                };

                return responseModel;
            }
        }

        [HttpPost]
        [Route("/user/forget-password")]
        public async Task<ResponseModel<string>> ForgetPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var token = await _userService.ForgetPasswordAsync(request.Email);

                // Prepare the response model with token
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "Token generated successfully!",
                    Data = token,
                    Status = true
                };

                return responseModel;
            }
            catch (UserException ex)
            {
                // Handle the exception and prepare the response model
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Data = null,
                    Status = false
                };

                return responseModel;
            }
            catch (Exception ex)
            {
                // Handle other exceptions and prepare the response model
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "An error occurred while processing your request.",
                    Data = null,
                    Status = false
                };

                return responseModel;
            }
        }

        [Authorize(AuthenticationSchemes = "UserValidationScheme")]
        [HttpPost]
        [Route("/user/reset-password")]
        public async Task<ResponseModel<string>> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                await _userService.ResetPassword(userId, request.NewPassword);

                // Prepare the response model for successful password reset
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "Password reset successful!",
                    Data = null,
                    Status = true
                };

                return responseModel;
            }
            catch (UserException ex)
            {
                // Handle the exception and prepare the response model
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Data = null,
                    Status = false
                };

                return responseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<string> responseModel = new ResponseModel<string>
                {
                    Message = "An error occurred while processing your request.",
                    Data = null,
                    Status = false
                };

                return responseModel;
            }
        }

    }
}
