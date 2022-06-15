using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Web.Services.Exceptions;
using Web.Services.Models;
using Web.Services.Users.Constants;
using Web.Services.Users.Interfaces;

namespace Web.Services.Users.Implementation
{
    internal class CreateUserService : ICreateUserService
    {
        private readonly IUserService _userService;

        public CreateUserService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ActionResult> CreateUserAsync(UpdateUserDto updateUserDto)
        {
            var result = new ActionResult();

            try
            {
                var userDto = await _userService.CreateUserAsync(updateUserDto);

                result.CreatedResult(userDto);
            }
            catch (RequestedResourceHasConflictException)
            {
                result.BadRequestResult(UsersConstants.UserExistsErrorMessage);
            }
            catch (Exception ex)
            {
                result.BadRequestResult(ex.Message);
            }

            return result;
        }
    }
}
