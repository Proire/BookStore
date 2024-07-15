using BookStoreBL;
using BookStoreML;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserModelLayer;

namespace BookStorePL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/createadmin")]
        public async Task<ResponseModel<User>> CreateAdmin([FromBody] UserRegistrationModel user)
        {
            try
            {
                // Set the role to "Admin" manually
                await _userService.CreateUserAsync(user, "Admin");

                // Prepare and return the response model
                ResponseModel<User> responseModel = new ResponseModel<User>
                {
                    Message = "Admin Added Successfully",
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
                    Status = false
                };
                return responseModel;
            }
        }
    }
}
