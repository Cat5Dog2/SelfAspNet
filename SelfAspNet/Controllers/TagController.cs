using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfAspNet.Helpers;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class TagController : Controller
{
    private readonly MyContext _db;
    private readonly ITagHelperComponentManager _manager;

    public TagController(MyContext db, ITagHelperComponentManager manager)
    {
        _db = db;
        _manager = manager;
    }

    public async Task<IActionResult> MyRadio(int? id = 1)
    {
        ViewBag.Pubs = _db.Books.Select(b => new SelectListItem
        {
            Value = b.Publisher,
            Text = b.Publisher
        }).Distinct();
        return View(await _db.Books.FindAsync(id));
    }

    public async Task<IActionResult> Link(int id = 1)
    {
        return View(await _db.Books.FindAsync(id));
    }

    // GET: TagController
    public ActionResult Index()
    {
        return View();
    }

    public IActionResult Cover()
    {
        ViewBag.Isbn = "978-4-7981-7556-0";
        return View();
    }

}

