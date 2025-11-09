using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobInterviewAPI.Attributes
{
    public class SimpleAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var header))
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            var auth = header.ToString();
            if (!auth.StartsWith("Basic "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            try
            {
                var encoded = auth.Substring(6).Trim();
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                var parts = decoded.Split(':', 2);
                if (parts.Length != 2 || parts[0] != "admin" || parts[1] != "admin")
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            await next();
        }
    }
}
