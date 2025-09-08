using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class ResultController : Controller
{
    public readonly MyContext _db;
    public ResultController(MyContext db)
    {
        _db = db;
    }

    public IActionResult AjaxForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AjaxSearch(string keyword, bool? released)
    {
        var bs = _db.Books.Select(b => b);
        if (!string.IsNullOrEmpty(keyword))
        {
            bs = bs.Where(b => b.Title.Contains(keyword));
        }
        if (released.HasValue && released.Value)
        {
            bs = bs.Where(b => b.Published <= DateTime.Now);
        }
        return PartialView("_AjaxResult", bs);
    }

    public IActionResult Move()
    {
        return Redirect("https://wings.msn.to/");
    }

    public IActionResult Local()
    {
        return LocalRedirect("/books");
    }

    public async Task<IActionResult> Status(int? id)
    {
        var bs = await _db.Books.FindAsync(id);
        if (bs == null)
        {
            return NotFound();
        }
        return View("../Books/Details", bs);
    }

    public IActionResult Nothing()
    {
        return Empty;
    }

    public IActionResult Plain()
    {
        return Content("こんにちは、世界！",
            System.Net.Mime.MediaTypeNames.Text.Plain, Encoding.UTF8);
    }
}
