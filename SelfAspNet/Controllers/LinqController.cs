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
        var bs = from b in _db.Books where b.Price < 3000 select b.Title;
        return View(bs);
    }
}
