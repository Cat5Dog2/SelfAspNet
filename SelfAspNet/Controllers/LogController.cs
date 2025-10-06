using System;
using Microsoft.AspNetCore.Mvc;

namespace SelfAspNet.Controllers;

public class LogController : Controller
{
    private readonly ILogger<LogController> _logger;
    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
    }

    public IActionResult Basic()
    {
        _logger.LogTrace("トレース");
        _logger.LogDebug("デバッグ");
        _logger.LogInformation("情報");
        _logger.LogWarning("警告");
        _logger.LogError("エラー");
        _logger.LogCritical("致命的な問題");

        return Content("ログはコンソールなどから確認してください");
    }

    public IActionResult Message()
    {
        _logger.LogWarning("{Path} -> {Current: yyyy年MM月dd日}", Request.Path, DateTime.Now);
        return Content("ログはコンソールなどから確認してください");
    }
}
