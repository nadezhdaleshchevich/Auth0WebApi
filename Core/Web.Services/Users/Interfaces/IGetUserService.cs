using Web.Services.Models;

namespace Web.Services.Users.Interfaces
{
    public interface IGetUserService
    {
        public Task<ActionResult> GetUserByAuth0IdAsync(string userAuth0Id);
    }
}
