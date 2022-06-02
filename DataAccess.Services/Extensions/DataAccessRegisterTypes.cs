using DataAccess.Extensions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.MapProfiles;
using DataAccess.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Services.Extensions
{
    public static class DataAccessServicesRegisterTypes
    {
        public static void LoadDataAccessServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.LoadDataAccessTypes();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(new []
                {
                    typeof(DataAccessServicesMapProfile)
                }));
        }
    }
}
