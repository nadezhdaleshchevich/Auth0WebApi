using DataAccess.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Users.Interfaces;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Factories.Interfaces;

namespace WebAPI.Controllers
{
    [Route(RoutersConstants.Users)]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IGetUserService _getService;
        private readonly ICreateUserService _createService;
        private readonly IUpdateUserService _updateService;
        private readonly IDeleteUserService _deleteService;
        private readonly IActionResultFactory _actionResultFactory;

        public UsersController(
            IGetUserService getUserService,
            ICreateUserService createUserService,
            IUpdateUserService updateUserService,
            IDeleteUserService deleteUserService,
            IActionResultFactory actionResultFactory)
        {
            _getService = getUserService ?? throw new ArgumentNullException(nameof(getUserService));
            _createService = createUserService ?? throw new ArgumentNullException(nameof(createUserService));
            _updateService = updateUserService ?? throw new ArgumentNullException(nameof(updateUserService));
            _deleteService = deleteUserService ?? throw new ArgumentNullException(nameof(deleteUserService));
            _actionResultFactory = actionResultFactory ?? throw new ArgumentNullException(nameof(actionResultFactory));
        }

        [HttpGet("{userAuth0Id}")]
        public async Task<IActionResult> GetUserAsync(string userAuth0Id)
        {
            var result = await _getService.GetUserByAuth0IdAsync(userAuth0Id);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UpdateUserDto updateUserDto)
        {
            var result = await _createService.CreateUserAsync(updateUserDto);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var result = await _updateService.UpdateUserAsync(userId, updateUserDto);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var result = await _deleteService.DeleteUserAsync(userId);

            return _actionResultFactory.CreateActionResult(result);
        }
    }
}
