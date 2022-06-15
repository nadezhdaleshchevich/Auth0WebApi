using Microsoft.Extensions.DependencyInjection;
using Web.Services.Companies.Exceptions;
using Web.Services.Users.Exceptions;

namespace Web.Services.Exceptions
{
    public static class WebServicesRegisterTypes
    {
        public static void LoadWebServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.LoadUsersWebServicesTypes();
            services.LoadCompaniesWebServicesTypes();
        }
    }
}
