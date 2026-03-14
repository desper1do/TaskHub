using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateUserRequestAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("request", out var requestArgument) || requestArgument == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }
        
        var nameProperty = requestArgument.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        
        if (nameProperty != null)
        {
            var nameValue = nameProperty.GetValue(requestArgument) as string;

            if (string.IsNullOrWhiteSpace(nameValue))
            {
                context.Result = new BadRequestObjectResult("Имя пользователя не задано");
                return;
            }
        }

        base.OnActionExecuting(context);
    }
}