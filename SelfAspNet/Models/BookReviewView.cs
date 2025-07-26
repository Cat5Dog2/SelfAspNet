using System.ComponentModel.DataAnnotations;

namespace SelfAspNet.Models;

public record BookReviewView(
    [property: Display(Name = "書籍名")] string Title,
    [property: Display(Name = "レビュー")] string ReviewBody
);
