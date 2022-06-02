using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserDto userDto)
        {
            try
            {
                await _userService.CreateUserAsync(userDto);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }


    }
}
