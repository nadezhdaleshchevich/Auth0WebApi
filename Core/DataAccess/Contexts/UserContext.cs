using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Contexts
{
    internal class UserContext : DomainContextBase<ApplicationDbContext>, IUserContext
    {
        public UserContext(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEntitySet<User> Users => GetDbSet<User>();
        public IEntitySet<Company> Companies => GetDbSet<Company>();
    }
}
