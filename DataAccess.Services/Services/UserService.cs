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

            var hasDbUser = await _context.Users.AnyAsync(i => i.Auth0Id == userDto.Auth0Id);

            if (hasDbUser)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbUser = _mapper.Map<DbUser>(userDto);

            _context.Users.Add(dbUser);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            var hasDbUser = await _context.Users.AnyAsync(i => i.Id != userId && i.Auth0Id == userDto.Auth0Id);

            if (hasDbUser)
            {
                throw new RequestedResourceHasConflictException();
            }

            _mapper.Map(userDto, dbUser);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _context.Users.Remove(dbUser);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> FindUserByIdAsync(int userId)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> FindUserByAuth0IdAsync(string userAuth0Id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Auth0Id == userAuth0Id);

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<UserDto>(dbUser);
        }
    }
}
