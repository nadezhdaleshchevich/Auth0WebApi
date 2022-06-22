using Web.Services.Models;

namespace Web.Services.Users.Interfaces
{
    public interface IDeleteUserService
    {
        Task<ActionResult> DeleteUserAsync(int userId);
    }
}
