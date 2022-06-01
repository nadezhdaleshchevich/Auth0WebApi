using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserContext _context;
        private readonly IMapper _mapper;

        public UserService(
            IUserContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var user = _mapper.Map<User>(userDto);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userDto.Id);

            if (user == null) throw new ArgumentException(); //TODO

            user = _mapper.Map<User>(userDto);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> FindUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (user == null) throw new ArgumentException(nameof(userId)); //TODO

            return _mapper.Map<UserDto>(user);
        }
    }
}
