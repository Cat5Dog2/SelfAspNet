using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SelfAspNet.Lib;

namespace SelfAspNet.Controllers;

public class ConfigController : Controller
{
    private readonly IConfiguration _config;
    private readonly MyAppOptions _app;

    public ConfigController(IConfiguration config, IOptions<MyAppOptions> app)
    {
        _config = config;
        _app = app.Value;
    }

    public IActionResult Basic()
    {
        ViewBag.Published = _config.GetValue<DateTime>("MyAppOptions:Published");
        ViewBag.Project = _config.GetValue<string>("MyAppOptions:Projects:0");
        return View();
    }

    public IActionResult Typed()
    {
        ViewBag.Published = _app.Published.ToLongDateString();
        ViewBag.Project = _app.Projects[0];
        return View("Basic");
    }
}
