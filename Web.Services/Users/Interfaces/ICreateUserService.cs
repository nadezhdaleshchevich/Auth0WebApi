using DataAccess.Services.Models;
using Web.Services.Models;

namespace Web.Services.Users.Interfaces
{
    public interface ICreateUserService
    {
        Task<ActionResult> CreateUserAsync(UpdateUserDto updateUserDto);
    }
}
