﻿using BookStoreBL;
using BookStoreML;
using BookStoreRL.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
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
        [Route("/registerUser")]
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
    }
}
