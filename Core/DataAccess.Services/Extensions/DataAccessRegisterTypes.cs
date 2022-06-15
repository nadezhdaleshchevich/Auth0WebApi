using DataAccess.Services.Implementation;
using DataAccess.Services.Interfaces;
using DataAccess.Services.MapProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Services.Extensions
{
    public static class DataAccessServicesRegisterTypes
    {
        public static void LoadDataAccessServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(new []
                {
                    typeof(DataAccessServicesMapProfile)
                }));
        }
    }
}
