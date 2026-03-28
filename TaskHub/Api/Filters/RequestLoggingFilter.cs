using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class RequestLoggingFilter : IActionFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger;
    private long _startedAt;

    public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _startedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var request = context.HttpContext.Request;
        _logger.LogInformation("Request started: {Method} {Path}", request.Method, request.Path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var elapsed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _startedAt;
        var statusCode = context.HttpContext.Response.StatusCode;
        _logger.LogInformation("Request finished: {StatusCode} in {Elapsed}ms", statusCode, elapsed);
    }
}