using System;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FailResponse = Infrastructure.WebApi.Models.FailResponse;

namespace Infrastructure.WebApi.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var response = new FailResponse
            {
                ErrorMessage = "Internal Error"
            };

            switch (exception)
            {
                case ApplicationMessageException messageException:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessage = messageException.Message;
                    break;
                case ApplicationValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessage = validationException.Message;
                    response.ErrorDetails = validationException.Errors;
                    break;
            }

            if(code == HttpStatusCode.InternalServerError)
                _logger.LogError($"Unhandled Exception Occurred: {exception}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }
}
