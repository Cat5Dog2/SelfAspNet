using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SelfAspNet.Models;

namespace SelfAspNet.Filters;

public class LogExceptionFilter : IAsyncExceptionFilter
{
    private readonly MyContext _db;
    public LogExceptionFilter(MyContext db)
    {
        _db = db;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        _db.ErrorLogs.Add(new ErrorLog
        {
            Path = context.HttpContext.Request.Path,
            Message = context.Exception.Message,
            Stacktrace = context.Exception.StackTrace ?? "",
            Accessed = DateTime.Now
        });
        await _db.SaveChangesAsync();

        context.ExceptionHandled = true;
        context.Result = new ViewResult
        {
            ViewName = "MyError"
        };
    }
}
