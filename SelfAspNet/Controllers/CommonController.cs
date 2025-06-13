using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class CommonController : Controller
{
    private readonly MyContext _db;
    public CommonController(MyContext db)
    {
        _db = db;
    }

    // GET: CommonController
    public ActionResult Index()
    {
        return View();
    }

    public IActionResult Another()
    {
        return View();
    }

    public IActionResult Nest()
    {
        return View();
    }

    public IActionResult List()
    {
        return View(_db.Books);
    }

    public IActionResult List2()
    {
        return View(_db.Books);
    }

}

