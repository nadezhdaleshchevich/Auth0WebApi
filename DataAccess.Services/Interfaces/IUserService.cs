using DataAccess.Services.Models;

namespace DataAccess.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UpdateUserDto userDto);
        Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int userId);
        Task<UserDto?> FindUserByIdAsync(int userId);
        Task<UserDto> FindUserByAuth0IdAsync(string userAuth0Id);
    }
}
