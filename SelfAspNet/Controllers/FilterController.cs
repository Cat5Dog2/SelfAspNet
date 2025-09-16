using System;
using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Filters;

namespace SelfAspNet.Controllers;

[MyLog]
public class FilterController : Controller
{
    [MyLogAsync]
    public IActionResult Index()
    {
        Console.WriteLine($"【Action】本体です。");
        ViewBag.Message = "こんにちは、世界！";
        return View();
    }
}
