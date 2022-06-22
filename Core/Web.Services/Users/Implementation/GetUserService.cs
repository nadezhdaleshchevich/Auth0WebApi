using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using Web.Services.Extensions;
using Web.Services.Models;
using Web.Services.Users.Constants;
using Web.Services.Users.Interfaces;

namespace Web.Services.Users.Implementation
{
    internal class GetUserService : IGetUserService
    {
        private readonly IUserService _userService;

        public GetUserService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ActionResult> GetUserByAuth0IdAsync(string userAuth0Id)
        {
            var result = new ActionResult();

            try
            {
                var userDto = await _userService.FindUserByAuth0IdAsync(userAuth0Id);

                result.OkResult(userDto);
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
