using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface IUserContext : IAsyncEntityStorage
    {
        public DbSet<User> Users { get; }
    }
}
