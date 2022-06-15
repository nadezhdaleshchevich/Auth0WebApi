using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Contexts
{
    internal class CompanyContext : DomainContextBase<ApplicationDbContext>, ICompanyContext
    {
        public CompanyContext(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEntitySet<Company> Companies => GetDbSet<Company>();
    }
}
