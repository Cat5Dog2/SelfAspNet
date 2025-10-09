using System;
using Microsoft.AspNetCore.Mvc;

namespace SelfAspNet.Controllers;

public class RouteController : Controller
{
    public IActionResult Param(int aid)
    {
        return Content($"記事番号：{aid}");
    }
    
    public IActionResult Constraint(string code)
    {
        return Content($"書籍：{code}");
    }
}
