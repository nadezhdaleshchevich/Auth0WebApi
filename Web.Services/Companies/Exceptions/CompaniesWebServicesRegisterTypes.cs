using Microsoft.Extensions.DependencyInjection;
using Web.Services.Companies.Implementation;
using Web.Services.Companies.Interfaces;

namespace Web.Services.Companies.Exceptions
{
    internal static class CompaniesWebServicesRegisterTypes
    {
        public static void LoadCompaniesWebServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IGetCompanyService, GetCompanyService>();
            services.AddTransient<ICreateCompanyService, CreateCompanyService>();
            services.AddTransient<IUpdateCompanyService, UpdateCompanyService>();
            services.AddTransient<IDeleteCompanyService, DeleteCompanyService>();
        }
    }
}
