using Infrastructure.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.WebApi.Extensions
{
    public static class ConfigureGlobalExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder ConfigureGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
