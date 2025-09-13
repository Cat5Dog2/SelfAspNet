using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using SelfAspNet.Lib;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class BinderController : Controller
{
    private readonly MyContext _db;
    private readonly IWebHostEnvironment _host;
    public BinderController(MyContext db, IWebHostEnvironment host)
    {
        _db = db;
        _host = host;
    }

    public IActionResult CreateMulti()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMulti(IEnumerable<Book> list)
    {
        for (var i = 0; i < list.Count(); i++)
        {
            var b = list.ElementAt(i);
            if (string.IsNullOrEmpty(b.Isbn))
            {
                foreach (var key in new[] { "Isbn", "Title", "Price", "Publisher", "Published" })
                {
                    ModelState.Remove($"list[{i}].{key}");
                }
                continue;
            }
            _db.Books.Add(b);
        }
        if (ModelState.IsValid)
        {
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(CreateMulti));
        }
        else
        {
            return View();
        }
    }

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(List<IFormFile>? upFiles)
    {
        if (upFiles == null || upFiles.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "ファイルが指定されていません。");
            return View();
        }

        var root = _host.WebRootPath ?? _host.ContentRootPath;
        var saveDir = Path.Combine(root, "Data");
        Directory.CreateDirectory(saveDir);

        var success = 0;
        foreach (var file in upFiles)
        {
            var safeName = Path.GetFileName(file.FileName);
            var savePath = Path.Combine(saveDir, safeName);

            var ext = new[] { ".jpg", ".jpeg", "png" };
            if (!ext.Contains(Path.GetExtension(safeName)))
            {
                ModelState.AddModelError(string.Empty, $"拡張子は.png、.jpgでなければなりません（{safeName}）");
                continue;
            }
            if (file.Length > 1024 * 1024)
            {
                ModelState.AddModelError(string.Empty, $"ファイルサイズは1MB以内でなければなりません（{safeName}）");
                continue;
            }

            await using var stream = System.IO.File.Create(savePath);
            await file.CopyToAsync(stream);

            success++;
        }

        ViewBag.Message = $"{success}個のファイルをアップロードしました。";
        return View();
    }
}
