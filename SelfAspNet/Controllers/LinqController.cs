using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class LinqController : Controller
{
    private readonly MyContext _db;
    public LinqController(MyContext db)
    {
        _db = db;
    }

    public IActionResult Basic()
    {
        var bs = _db.Books.Where(b => b.Price < 3000).Select(b => b.Title);
        return View(bs);
    }

    public IActionResult Contains()
    {
        string searchText = "JavaScript";
        ViewBag.SearchText = searchText;
        var bs = _db.Books.Where(b => b.Title.Contains(searchText));
        return View("TitleSearch", bs);
    }

    public IActionResult StartWith()
    {
        string searchText = "独習";
        ViewBag.SearchText = searchText;
        var bs = _db.Books.Where(b => b.Title.StartsWith(searchText));
        return View("TitleSearch", bs);
    }

    public IActionResult Selection()
    {
        var bs = _db.Books.Where(b => new int[] { 3, 9 }.Contains(b.Published.Month));
        return View("List", bs);
    }

    public IActionResult Between()
    {
        var bs = _db.Books.Where(b => 4000 <= b.Price && b.Price <= 4500);
        return View("Items", bs);
    }
}
