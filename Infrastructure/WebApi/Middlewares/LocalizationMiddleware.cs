using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.String;

namespace Infrastructure.WebApi.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            var defaultLang = IsNullOrEmpty(lang) ? "en-US" : lang;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLang);

            await _next(context);
        }
    }
}