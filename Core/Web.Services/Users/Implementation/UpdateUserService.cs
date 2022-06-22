using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Web.Services.Extensions;
using Web.Services.Models;
using Web.Services.Users.Constants;
using Web.Services.Users.Interfaces;

namespace Web.Services.Users.Implementation
{
    internal class UpdateUserService : IUpdateUserService
    {
        private readonly IUserService _userService;

        public UpdateUserService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ActionResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var result = new ActionResult();

            try
            {
                var userDto = await _userService.UpdateUserAsync(userId, updateUserDto);

                result.OkResult(userDto);
            }
            catch (RequestedResourceHasConflictException)
            {
                result.BadRequestResult(UsersConstants.UserExistsErrorMessage);
            }
            catch (RequestedResourceNotFoundException)
            {
                result.NotFoundResult(UsersConstants.UserDoesNotFindErrorMessage);
            }
            catch (Exception ex)
            {
                result.BadRequestResult(ex.Message);
            }

            return result;
        }
    }
}
