using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NuGet.Common;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class ResultController : Controller
{
    private readonly MyContext _db;
    private readonly IWebHostEnvironment _host;
    public ResultController(MyContext db, IWebHostEnvironment host)
    {
        _db = db;
        _host = host;
    }

    public IActionResult AjaxForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AjaxSearch(string keyword, bool? released)
    {
        var bs = _db.Books.Select(b => b);
        if (!string.IsNullOrEmpty(keyword))
        {
            bs = bs.Where(b => b.Title.Contains(keyword));
        }
        if (released.HasValue && released.Value)
        {
            bs = bs.Where(b => b.Published <= DateTime.Now);
        }
        return PartialView("_AjaxResult", bs);
    }

    public IActionResult Move()
    {
        return Redirect("https://wings.msn.to/");
    }

    public IActionResult Local()
    {
        return LocalRedirect("/books");
    }

    public async Task<IActionResult> Status(int? id)
    {
        var bs = await _db.Books.FindAsync(id);
        if (bs == null)
        {
            return NotFound();
        }
        return View("../Books/Details", bs);
    }

    public IActionResult Nothing()
    {
        return Empty;
    }

    public IActionResult Plain()
    {
        return Content("こんにちは、世界！",
            System.Net.Mime.MediaTypeNames.Text.Plain, Encoding.UTF8);
    }

    public async Task<IActionResult> Csv()
    {
        var bs = await _db.Books.ToListAsync();
        var data = new StringBuilder();

        bs.ForEach(b => data.Append(string.Format(
            $"{b.Id},{b.Isbn},{b.Title},{b.Price},{b.Publisher},{b.Published}\r\n"
        )));

        Response.Headers.Append("Content-Disposition", "attachment;filename=data.csv");

        return Content(data.ToString(), "text/comma-separated-values",
            Encoding.GetEncoding("Shift_JIS"));
    }

    public IActionResult Image(int id)
    {
        var path = $"/images/img_{id}.png";
        var fullpath = _host.WebRootPath + path;
        return File(path, "image/png",
            new DateTimeOffset(System.IO.File.GetLastWriteTime(fullpath)),
            new EntityTagHeaderValue(ComputeSha256(fullpath))
        );
    }

    private static string ComputeSha256(string path)
    {
        using var sha = SHA512.Create();
        using var stream = new FileStream(path, FileMode.Open);
        var bs = sha.ComputeHash(stream);
        var result = new StringBuilder();
        foreach (var b in bs)
        {
            result.Append(b.ToString("x2"));
        }
        return $"\"{result.ToString()}\"";
    }

    public async Task<IActionResult> Photo(int id = 1)
    {
        var p = await _db.Photos.FindAsync(id);
        if (p == null)
        {
            return NotFound();
        }
        return File(p.Content, p.ContentType, p.Name);
    }

    public IActionResult Pdf()
    {
        var stream = new MemoryStream();
        var doc = new iText.Layout.Document(
            new PdfDocument(
                new PdfWriter(stream)
            )
        );

        var font = PdfFontFactory.CreateFont("HeiseiKakuGo-W5", "UniJIS-UCS2-H");
        doc.SetFont(font);

        doc.Add(
            new Paragraph("こんにちは、").Add(new Text("世界！"))
                .SetFontSize(20).SetFontColor(new DeviceRgb(255, 0, 0))
        );

        doc.Close();
        return File(stream.ToArray(), MediaTypeNames.Application.Pdf);
    }
}
