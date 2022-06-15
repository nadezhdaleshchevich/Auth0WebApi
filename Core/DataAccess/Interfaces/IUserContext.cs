using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IUserContext : IAsyncEntityStorage
    {
        public IEntitySet<User> Users { get; }
        public IEntitySet<Company> Companies { get; }
    }
}
