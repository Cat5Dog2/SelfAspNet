using System;
using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Filters;

namespace SelfAspNet.Controllers;

public class FilterController : Controller
{
    [MyLog]
    public IActionResult Index()
    {
        Console.WriteLine($"【Action】本体です。");
        ViewBag.Message = "こんにちは、世界！";
        return View();
    }
}
