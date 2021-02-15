using Microsoft.AspNetCore.Mvc.Filters;
using Infrastructure.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebApi.Filters
{
    public class SuccessfulResponseWrapperFilter : IActionFilter
    {
        public  void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) 
                return;

            context.HttpContext.Response.ContentType = "application/json";

            var result = ((ObjectResult)context.Result);

            result.Value = new OkResponse
            {
                Data = result.Value
            };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
