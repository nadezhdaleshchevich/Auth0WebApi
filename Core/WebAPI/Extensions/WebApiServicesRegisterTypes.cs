using WebAPI.Factories.Implementation;
using WebAPI.Factories.Interfaces;

namespace WebAPI.Extensions
{
    public static class WebApiServicesRegisterTypes
    {
        public static void LoadWebApiServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IActionResultFactory, ActionResultFactory>();
        }
    }
}
