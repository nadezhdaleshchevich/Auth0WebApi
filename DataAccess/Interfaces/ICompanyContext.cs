using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface ICompanyContext : IAsyncEntityStorage
    {
        public DbSet<Company> Companies { get; }
    }
}
