using System;
using Microsoft.AspNetCore.Mvc;

namespace SelfAspNet.Controllers;

public class ConfigController : Controller
{
    private readonly IConfiguration _config;
    public ConfigController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Basic()
    {
        ViewBag.Published = _config.GetValue<DateTime>("MyAppOptions:Published");
        ViewBag.Project = _config.GetValue<string>("MyAppOptions:Projects:0");
        return View();
    }
}
