using Api.Controllers.Tasks.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ValidateSetTaskTitleRequestFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.ActionArguments.Values.OfType<SetTaskTitleRequest>().FirstOrDefault();

        if (request is null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            context.Result = new BadRequestObjectResult("Название задачи не задано");
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}