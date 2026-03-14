using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ResponseTimeHeaderAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        context.HttpContext.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            context.HttpContext.Response.Headers.Append("X-Response-Time-Ms", stopwatch.ElapsedMilliseconds.ToString());
            return Task.CompletedTask;
        });

        await next();
    }
}