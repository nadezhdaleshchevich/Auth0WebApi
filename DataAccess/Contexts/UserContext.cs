using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    internal class UserContext : IUserContext
    {
        private readonly ApplicationDbContext _dbContext;

        public UserContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<User> Users => _dbContext.Users;

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
