using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SelfAspNet.Models;

public class Author
{
    public int Id { get; set; }
    [Display(Name = "ペンネーム")]
    public string PenName { get; set; } = String.Empty;
    [Display(Name = "ユーザー")]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
