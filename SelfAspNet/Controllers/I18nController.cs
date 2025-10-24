using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace SelfAspNet.Controllers;

public class I18nController : Controller
{
    public IActionResult Basic()
    {
        return View();
    }
}
