using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SelfAspNet.Models;

public record MiniBook
(
    [property: Display(Name = "書名")] string Title,
    [property: Display(Name = "価格")] int Price
);
