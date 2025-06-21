using System;
using System.ComponentModel.DataAnnotations;

namespace SelfAspNet.Models;

public class Book
{
    [Display(Name = "ID")]
    public int Id { get; set; }
    [Display(Name = "ISBN")]
    [DataType(DataType.ImageUrl)]
    public string Isbn { get; set; } = String.Empty;
    [Display(Name = "タイトル")]
    public string Title { get; set; } = String.Empty;
    [DataType(DataType.Currency)]
    [Display(Name = "価格")]
    public int Price { get; set; }
    [Display(Name = "出版社")]
    public string Publisher { get; set; } = String.Empty;
    [Display(Name = "刊行日")]
    [DataType(DataType.Date)]
    public DateTime Published { get; set; }
    [Display(Name = "配布サンプル")]
    public bool Sample { get; set; }

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}
