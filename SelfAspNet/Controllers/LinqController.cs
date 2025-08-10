using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Models;

namespace SelfAspNet.Controllers;

public class LinqController : Controller
{
    private readonly MyContext _db;
    public LinqController(MyContext db)
    {
        _db = db;
    }

    public IActionResult Basic()
    {
        var bs = _db.Books.Where(b => b.Price < 3000).Select(b => b.Title);
        return View(bs);
    }

    public IActionResult Contains()
    {
        string searchText = "JavaScript";
        ViewBag.SearchText = searchText;
        var bs = _db.Books.Where(b => b.Title.Contains(searchText));
        return View("TitleSearch", bs);
    }

    public IActionResult StartWith()
    {
        string searchText = "独習";
        ViewBag.SearchText = searchText;
        var bs = _db.Books.Where(b => b.Title.StartsWith(searchText));
        return View("TitleSearch", bs);
    }

    public IActionResult Selection()
    {
        var bs = _db.Books.Where(b => new int[] { 3, 9 }.Contains(b.Published.Month));
        return View("List", bs);
    }

    public IActionResult Between()
    {
        var bs = _db.Books.Where(b => 4000 <= b.Price && b.Price <= 4500);
        return View("Items", bs);
    }

    public IActionResult Regex()
    {
        var reg = new Regex("\\d");
        var bs = _db.Books.AsEnumerable().Where(b => reg.IsMatch(b.Title)).ToList();
        return View("List", bs);
    }

    public async Task<IActionResult> Single()
    {
        var bs = await _db.Books.SingleAsync(b => b.Isbn == "978-4-7981-8094-6");
        return Content(bs.Title);
    }

    public async Task<IActionResult> Exists()
    {
        var bs = await _db.Books.AnyAsync(b => b.Price >= 4000);
        return Content(bs.ToString());
    }

    public IActionResult Filter(string keyword, bool? released)
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
        return View(bs);
    }

    public IActionResult Order()
    {
        var bs = _db.Books.OrderByDescending(b => b.Price).ThenBy(b => b.Published);
        return View("List", bs);
    }

    public IActionResult SortGrid(string sort)
    {
        ViewBag.Isbn = sort == "Isbn" ? "dIsbn" : "Isbn";
        ViewBag.Title = string.IsNullOrEmpty(sort) ? "dTitle" : "";
        ViewBag.Price = sort == "Price" ? "dPrice" : "Price";
        ViewBag.Publisher = sort == "Publisher" ? "dPublisher" : "Publisher";
        ViewBag.Published = sort == "Published" ? "dPublished" : "Published";
        ViewBag.Sample = sort == "Sample" ? "dSample" : "Sample";

        var bs = _db.Books.Select(b => b);
        bs = sort switch
        {
            "Isbn" => bs.OrderBy(b => b.Isbn),
            "Title" => bs.OrderBy(b => b.Title),
            "Price" => bs.OrderBy(b => b.Price),
            "Publisher" => bs.OrderBy(b => b.Publisher),
            "Published" => bs.OrderBy(b => b.Published),
            "Sample" => bs.OrderBy(b => b.Sample),
            "dIsbn" => bs.OrderByDescending(b => b.Isbn),
            "dTitle" => bs.OrderByDescending(b => b.Title),
            "dPrice" => bs.OrderByDescending(b => b.Price),
            "dPublisher" => bs.OrderByDescending(b => b.Publisher),
            "dPublished" => bs.OrderByDescending(b => b.Published),
            "dSample" => bs.OrderByDescending(b => b.Sample),
            _ => bs.OrderBy(b => b.Title),
        };
        return View(bs);
    }

    public IActionResult Select()
    {
        var bs = _db.Books.OrderByDescending(b => b.Published)
            .Select(b => new SummaryBookView(
                b.Title.Substring(0, 7) + "...",
                (int)(b.Price * 0.9),
                b.Published <= DateTime.Now ? "発売中" : "発売予定"
        ));
        return View(bs);
    }

    public IActionResult Skip()
    {
        var bs = _db.Books.OrderBy(b => b.Published).Skip(2).Take(3);
        return View("List", bs);
    }

    public IActionResult Page(int id = 1)
    {
        var pageSize = 3;
        var pageNum = id - 1;
        var bs = _db.Books.OrderBy(b => b.Published)
            .Skip(pageSize * pageNum)
            .Take(pageSize);
        return View("List", bs);
    }

    public async Task<IActionResult> First()
    {
        var bs = await _db.Books.OrderBy(b => b.Published).FirstAsync();
        return View("Details", bs);
    }

    public IActionResult Group()
    {
        var bs = _db.Books.GroupBy(b => b.Publisher);
        return View(bs);
    }

    public IActionResult GroupMini()
    {
        var bs = _db.Books.GroupBy(
            b => b.Publisher,
            b => new MiniBook(b.Title, b.Price)
        );
        return View(bs);
    }

    public IActionResult GroupMulti()
    {
        var bs = _db.Books.GroupBy(b => new BookGroup(b.Publisher, b.Published.Year));
        return View(bs);
    }

    public IActionResult Having()
    {
        var bs = _db.Books.GroupBy(b => b.Publisher)
            .Where(group => group.Average(b => b.Price) >= 3000)
            .Select(group => new HavingBook(group.Key, (int)group.Average(b => b.Price)));
        return View(bs);
    }

    public IActionResult HavingSort()
    {
        var bs = _db.Books.GroupBy(b => b.Publisher)
            .OrderBy(group => group.Average(b => b.Price))
            .Select(group => new HavingBook(group.Key, (int)group.Average(b => b.Price)));
        return View("Having", bs);
    }

    public IActionResult Join()
    {
        var rs = _db.Books
            .Join(_db.Reviews, b => b.Id, rev => rev.BookId,
            (b, rev) => new BookReviewView(b.Title, rev.Body));
        return View(rs);
    }

    public async Task<IActionResult> Update()
    {
        await _db.Books.Where(b => b.Publisher == "翔泳社")
            .ExecuteUpdateAsync(setters => setters.SetProperty(
                b => b.Price, b => (int)(b.Price * 0.8)
            ));
        return Content("更新しました。");
    }

    public async Task<IActionResult> Insert()
    {
        _db.Reviews.Add(new Review
        {
            Name = "藤井友美",
            Body = "しっかり勉強したい人向けの本です。最初に、超初心者本を読んで、2冊目にこの本を読むことをおすすめします。",
            LastUpdated = new DateTime(2024, 05, 17),
            Book = new Book
            {
                Isbn = "978-4-7981-6849-4",
                Title = "独習PHP",
                Price = 3740,
                Publisher = "翔泳社",
                Published = new DateTime(2021, 06, 14),
                Sample = true
            }
        });
        await _db.SaveChangesAsync();
        return Content("データを追加しました。");
    }

    public async Task<IActionResult> Insert2()
    {
        var book = await _db.Books.FindAsync(1);
        _db.Reviews.Add(new Review
        {
            Name = "木村裕二",
            Body = "最近は、意外と書き方が変わっていて勉強になった。",
            LastUpdated = new DateTime(2024, 06, 03),
            Book = book!
        });
        await _db.SaveChangesAsync();
        return Content("レビューを追加しました。");
    }
}
