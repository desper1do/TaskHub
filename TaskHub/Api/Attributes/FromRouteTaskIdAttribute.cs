using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class FromRouteTaskIdAttribute : ModelBinderAttribute
{
    public FromRouteTaskIdAttribute()
    {
        BinderType = typeof(TaskIdModelBinder);
        BindingSource = BindingSource.Path;
        Name = "id";
    }
}