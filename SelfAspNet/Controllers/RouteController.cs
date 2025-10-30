using System;
using Microsoft.AspNetCore.Mvc;

namespace SelfAspNet.Controllers;

//[Route("chap08")]
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

    public IActionResult Limit()
    {
        return Content("キャンペーン期間中です！");
    }

    //[Route("d/{id:int=13}")]
    public IActionResult Attr(int id)
    {
        return Content($"記事番号：{id}");
    }
}
