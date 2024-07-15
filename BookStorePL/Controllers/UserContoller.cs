using BookStoreBL;
using BookStoreML;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("/register")]
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
        [Route("/login")]
        public async Task<ResponseModel<User>> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                // Call the asynchronous Login method from the business logic layer (BLL)
                User user = await _userService.LoginUserAsync(model);

                // Prepare the response model with user data
                ResponseModel<User> responseModel = new ResponseModel<User>
                {
                    Message = "Logged In Successfully!",
                    Data = user, // Return the user object
                    Status = true
                };

                return responseModel;
            }
            catch (UserException ex)
            {
                // Handle the exception and prepare the response model
                ResponseModel<User> responseModel = new ResponseModel<User>
                {
                    Message = ex.Message,
                    Data = null, 
                    Status = false
                };

                return responseModel;
            }
        }
    }
}
