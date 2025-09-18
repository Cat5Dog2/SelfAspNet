using System;
using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Filters;

namespace SelfAspNet.Controllers;

[MyLog]
[MyControllerFilter(Order = int.MinValue)]
public class FilterController : Controller
{
    [MyLogAsync]
    public IActionResult Index()
    {
        Console.WriteLine($"【Action】本体です。");
        ViewBag.Message = "こんにちは、世界！";
        return View();
    }

    [TimeLimit("2024/05/01", "2024/07/15")]
    public IActionResult Range()
    {
        return Content("キャンペーン期間中です");
    }

    [ServiceFilter(typeof(LogExceptionFilter))]
    public IActionResult Except()
    {
        throw new Exception("問題が発生しました！");
    }
}
