using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface ICompanyContext : IAsyncEntityStorage
    {
        public IEntitySet<Company> Companies { get; }
    }
}
