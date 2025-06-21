using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAspNet.Models;

public class User
{
    public int Id { get; set; }
    [Display(Name = "名前")]
    public string Name { get; set; } = String.Empty;
    [Display(Name = "メールアドレス")]
    public string? Email { get; set; }
    [Display(Name = "メールアドレス（確認）")]
    public string? EmailConfirmd { get; set; }
    [Display(Name = "誕生日")]
    public DateTime Birth { get; set; } = DateTime.Now;
    [Display(Name = "ニュース希望")]
    public bool NeedNews { get; set; }

    public virtual Author? Author { get; set; }
}
