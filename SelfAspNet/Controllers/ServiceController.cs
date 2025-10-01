using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Lib;

namespace SelfAspNet.Controllers;

public class ServiceController : Controller
{
    private readonly IMyService1 _svc1_1;
    private readonly IMyService1 _svc1_2;
    private readonly IMyService2 _svc2_1;
    private readonly IMyService2 _svc2_2;
    private readonly IMyService3 _svc3_1;
    private readonly IMyService3 _svc3_2;
    private readonly IMessageService _msg;
    private readonly IEnumerable<IMessageService> _multi;

    public ServiceController(
        IMyService1 svc1_1, IMyService1 svc1_2,
        IMyService2 svc2_1, IMyService2 svc2_2,
        IMyService3 svc3_1, IMyService3 svc3_2,
        IMessageService msg, IEnumerable<IMessageService> multi)
    {
        _svc1_1 = svc1_1;
        _svc1_2 = svc1_2;
        _svc2_1 = svc2_1;
        _svc2_2 = svc2_2;
        _svc3_1 = svc3_1;
        _svc3_2 = svc3_2;
        _msg = msg;
        _multi = multi;
    }


    public ActionResult Scope()
    {
        string lb = Environment.NewLine;
        string content = $"Singleton：{_svc1_1.Id.ToString()} / {_svc1_2.Id.ToString()}{lb}";
        content += $"Scoped：{_svc2_1.Id.ToString()} / {_svc2_2.Id.ToString()}{lb}";
        content += $"Transient：{_svc3_1.Id.ToString()} / {_svc3_2.Id.ToString()}";
        return Content(content);
    }

    public IActionResult Multi()
    {
        var list = new List<string>();
        foreach (var msg in _multi) { list.Add(msg.Message); }
        return Content(string.Join(", ", list));
    }

}

