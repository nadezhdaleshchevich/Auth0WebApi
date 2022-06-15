using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Extensions
{
    public static class AuthenticationOptionsExtensions
    {
        public static void LoadAuthenticationOptions(this AuthenticationOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
