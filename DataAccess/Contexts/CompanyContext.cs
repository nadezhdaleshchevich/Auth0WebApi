using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    internal class CompanyContext : ICompanyContext
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<Company> Companies => _dbContext.Companies;

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
