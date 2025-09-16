using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SelfAspNet.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class MyLogAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"【Before】{context.ActionDescriptor.DisplayName}が実行されます。");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"【After】{context.ActionDescriptor.DisplayName}が実行されました。");
    }
}
