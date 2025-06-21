using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class EntityController : Controller
{
    private readonly MyContext _db;
    public EntityController(MyContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Assoc(int id = 1)
    {
        var b = await _db.Books.FindAsync(id);
        return View(b);
    }
}
