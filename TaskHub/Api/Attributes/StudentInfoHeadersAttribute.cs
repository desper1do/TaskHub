using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class StudentInfoHeadersAttribute : ActionFilterAttribute
{
    private readonly string _studentName;
    private readonly string _studentGroup;

    public StudentInfoHeadersAttribute(string studentName, string studentGroup)
    {
        _studentName = studentName;
        _studentGroup = studentGroup;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Response.Headers.Append("X-Student-Name", _studentName);
        context.HttpContext.Response.Headers.Append("X-Student-Group", _studentGroup);
        
        base.OnActionExecuting(context);
    }
}