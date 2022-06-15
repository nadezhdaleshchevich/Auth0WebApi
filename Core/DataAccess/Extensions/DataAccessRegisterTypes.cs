using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class DataAccessRegisterTypes
    {
        public static void LoadDataAccessTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<ICompanyContext, CompanyContext>();
        }
    }
}
