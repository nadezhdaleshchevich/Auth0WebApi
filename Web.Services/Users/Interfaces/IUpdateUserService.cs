using DataAccess.Services.Models;
using Web.Services.Models;

namespace Web.Services.Users.Interfaces
{
    public interface IUpdateUserService
    {
        Task<ActionResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
    }
}
