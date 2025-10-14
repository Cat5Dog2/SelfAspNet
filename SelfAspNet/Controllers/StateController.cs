using System;
using Microsoft.AspNetCore.Mvc;

namespace SelfAspNet.Controllers;

public class StateController : Controller
{
    public IActionResult Cookie()
    {
        ViewBag.Email = HttpContext.Request.Cookies["email"];
        return View();
    }

    [HttpPost]
    public IActionResult Cookie(string email)
    {
        HttpContext.Response.Cookies.Append("email", email, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true
        });

        return RedirectToAction("Cookie");
    }
}
