using System.ComponentModel.DataAnnotations;

namespace SelfAspNet.Models;

public record HavingBook(
    [property: Display(Name = "出版社")] string Publisher,
    [property: Display(Name = "平均価格")] int PriceAverage
);
