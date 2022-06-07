using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;

namespace WebAPI.Controllers
{
    [Route(RoutersConstants.Users)]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userAuth0Id}")]
        public async Task<IActionResult> GetUserAsync(string userAuth0Id)
        {
            IActionResult result;

            try
            {
                var userDto = await _userService.FindUserByAuth0IdAsync(userAuth0Id);

                result = new OkObjectResult(userDto);
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "User doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UpdateUserDto updateUserDto)
        {
            IActionResult result;

            try
            {
                var userDto = await _userService.CreateUserAsync(updateUserDto);

                result = new OkObjectResult(userDto);
            }
            catch (RequestedResourceHasConflictException)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = "User already exists"
                });
            }
            catch(Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            IActionResult result;

            try
            {
                var userDto = await _userService.UpdateUserAsync(userId, updateUserDto);

                result = new OkObjectResult(userDto);
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "User doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            IActionResult result;

            try
            {
                await _userService.DeleteUserAsync(userId);

                result = new OkResult();
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "User doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }
    }
}
