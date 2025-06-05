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

}

