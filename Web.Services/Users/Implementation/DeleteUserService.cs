using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using Web.Services.Exceptions;
using Web.Services.Models;
using Web.Services.Users.Constants;
using Web.Services.Users.Interfaces;

namespace Web.Services.Users.Implementation
{
    internal class DeleteUserService : IDeleteUserService
    {
        private readonly IUserService _userService;

        public DeleteUserService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            var result = new ActionResult();

            try
            {
                await _userService.DeleteUserAsync(userId);

                result.OkResult();
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
