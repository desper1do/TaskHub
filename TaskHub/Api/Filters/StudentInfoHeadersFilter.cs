using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class StudentInfoHeadersFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers["X-Student-Name"] = "Nasibulin Danila";
        context.HttpContext.Response.Headers["X-Student-Group"] = "RI-240943";
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}