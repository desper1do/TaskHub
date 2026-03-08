namespace Api.Middleware;

public class StudentInfoMiddleware
{
    private readonly RequestDelegate _next;
    public StudentInfoMiddleware(RequestDelegate next) { _next = next; }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Student-Name", "Danila Nasibulin");
        context.Response.Headers.Append("X-Student-Group", "RI-240943");
        await _next(context);
    }
}