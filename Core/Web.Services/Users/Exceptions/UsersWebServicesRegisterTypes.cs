using Microsoft.Extensions.DependencyInjection;
using Web.Services.Users.Implementation;
using Web.Services.Users.Interfaces;

namespace Web.Services.Users.Exceptions
{
    internal static class UsersWebServicesRegisterTypes
    {
        public static void LoadUsersWebServicesTypes(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IGetUserService, GetUserService>();
            services.AddTransient<ICreateUserService, CreateUserService>();
            services.AddTransient<IUpdateUserService, UpdateUserService>();
            services.AddTransient<IDeleteUserService, DeleteUserService>();
        }
    }
}
