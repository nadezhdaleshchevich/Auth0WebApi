using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Extensions
{
    public static class JwtBearerOptionsExtensions
    {
        public static void LoadJwtBearerOptions(this JwtBearerOptions options, ConfigurationManager config)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (config == null) throw new ArgumentNullException(nameof(config));

            options.Authority = config["Auth0:Domain"];
            options.Audience = config["Auth0:Audience"];
        }
    }
}
