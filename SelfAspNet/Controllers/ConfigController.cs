using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SelfAspNet.Lib;

namespace SelfAspNet.Controllers;

public class ConfigController : Controller
{
    private readonly IConfiguration _config;
    private readonly MyAppOptions _app;
    private readonly ApiInfoOptions _slide;
    private readonly ApiInfoOptions _weather;

    public ConfigController(IConfiguration config, IOptions<MyAppOptions> app,
        IOptionsSnapshot<ApiInfoOptions> api)
    {
        _config = config;
        _app = app.Value;
        _slide = api.Get(ApiInfoOptions.SlideShow);
        _weather = api.Get(ApiInfoOptions.OpenWeather);
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

    public IActionResult Named()
    {
        string lb = Environment.NewLine;
        string content = $"SlideShow API: {_slide.BaseUrl}{lb}";
        content += $"OpenWeather API: {_weather.BaseUrl}";
        return Content(content);
    }
}
