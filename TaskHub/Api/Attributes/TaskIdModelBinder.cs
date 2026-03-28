using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes;

public sealed class TaskIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue("id").FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.ModelState.AddModelError("id", "Идентификатор задачи не задан");
            return Task.CompletedTask;
        }

        if (!Guid.TryParse(value, out var id))
        {
            bindingContext.ModelState.AddModelError("id", "Идентификатор задачи имеет некорректный формат");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(id);
        return Task.CompletedTask;
    }
}