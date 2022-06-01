using DataAccess.Services.Models;

namespace DataAccess.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(string userId);
        Task<UserDto> FindUserByIdAsync(string userId);
    }
}
