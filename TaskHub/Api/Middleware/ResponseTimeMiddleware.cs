using System.Diagnostics;

namespace Api.Middleware;

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;
    public ResponseTimeMiddleware(RequestDelegate next) { _next = next; }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context); 
        stopwatch.Stop();
        
        if (!context.Response.HasStarted)
        {
            context.Response.Headers.Append("X-Response-Time-Ms", stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}