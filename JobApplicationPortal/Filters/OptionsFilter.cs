using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace JobApplicationPortal.Filters
{
    public class OptionsFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Options)
            {
                context.HttpContext.Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, PATCH, DELETE, OPTIONS");
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
                context.Result = new OkResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No additional logic needed after execution
        }
    }
}

