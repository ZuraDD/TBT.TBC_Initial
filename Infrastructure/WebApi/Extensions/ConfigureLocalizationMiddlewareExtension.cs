using Infrastructure.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.WebApi.Extensions
{
    public static class ConfigureLocalizationMiddlewareExtension
    {
        public static IApplicationBuilder ConfigureLocalizationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
