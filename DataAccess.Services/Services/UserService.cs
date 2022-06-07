using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;
using DbUser = DataAccess.Models.User;

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

        public async Task<UserDto> CreateUserAsync(UpdateUserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var dbUsers = await _context.Users.Where(i => i.Auth0Id == userDto.Auth0Id).ToArrayAsync();

            if (dbUsers.Length != 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var userDb = _mapper.Map<DbUser>(userDto);

            _context.Users.Add(userDb);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(userDb);
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var userDb = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (userDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _mapper.Map(userDto, userDb);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(userDb);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (userDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _context.Users.Remove(userDb);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> FindUserByIdAsync(int userId)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (userDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<UserDto>(userDb);
        }

        public async Task<UserDto> FindUserByAuth0IdAsync(string userAuth0Id)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(i => i.Auth0Id == userAuth0Id);

            if (userDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<UserDto>(userDb);
        }
    }
}
