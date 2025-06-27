using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SelfAspNet.Models;

public class Review
{
    public int Id { get; set; }
    [Display(Name ="名前")]
    public string Name { get; set; } = String.Empty;
    [Display(Name ="レビュー")]
    public string Body { get; set; } = String.Empty;
    [Display(Name ="更新日")]
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    [Display(Name ="書籍")]
    public virtual int BookId { get; set; }
    public virtual Book Book { get; set; } = null!;
}
