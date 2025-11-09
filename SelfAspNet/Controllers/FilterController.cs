using System;
using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Filters;
using SelfAspNet.Lib;

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

    [LogException]
    public IActionResult Except()
    {
        throw new Exception("問題が発生しました！");
    }

    public IActionResult Csrf()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Process()
    {
        return Content("処理終了");
    }

    [RefererSelector(false)]
    public IActionResult Referer()
    {
        return Content("正しくアクセス出来ました。");
    }
}
