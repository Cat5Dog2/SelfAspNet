using System;

namespace SelfAspNet.Models;

public class AuthorBook
{
    public virtual Author Author { get; set; } = null!;
    public virtual Book Book { get; set; } = null!;
}
