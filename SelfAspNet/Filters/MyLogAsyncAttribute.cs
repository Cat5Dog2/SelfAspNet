using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SelfAspNet.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class MyLogAsyncAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine($"【asyncBefore】{context.ActionDescriptor.DisplayName}が実行されます。");
        await next();
        Console.WriteLine($"【asyncAfter】{context.ActionDescriptor.DisplayName}が実行されました。");
    }
}
