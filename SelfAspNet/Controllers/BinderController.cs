using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using SelfAspNet.Lib;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class BinderController : Controller
{
    private readonly MyContext _db;
    private readonly IWebHostEnvironment _host;
    public BinderController(MyContext db, IWebHostEnvironment host)
    {
        _db = db;
        _host = host;
    }

    public IActionResult CreateMulti()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMulti(IEnumerable<Book> list)
    {
        for (var i = 0; i < list.Count(); i++)
        {
            var b = list.ElementAt(i);
            if (string.IsNullOrEmpty(b.Isbn))
            {
                foreach (var key in new[] { "Isbn", "Title", "Price", "Publisher", "Published" })
                {
                    ModelState.Remove($"list[{i}].{key}");
                }
                continue;
            }
            _db.Books.Add(b);
        }
        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(CreateMulti));
        }
        else
        {
            return View();
        }
    }
}
