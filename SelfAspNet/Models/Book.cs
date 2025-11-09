using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using SelfAspNet.Lib;

namespace SelfAspNet.Models;

[XmlRoot("Book")]
public class Book
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    // [RegularExpression("978-4-[0-9]{2,5}-[0-9]{2,5}-[0-9X]",
    //     ErrorMessage = "{0}の形式が誤っています。")]
    // [Remote(action: "UniqueIsbn", controller: "Books", AdditionalFields = nameof(Id))]
    // [Display(Name = "ISBN")]
    // [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "RequiredError")]
    [Display(Name = "Book_Isbn")]
    [XmlElement("Isbn")]
    public string Isbn { get; set; } = String.Empty;

    // [Required(ErrorMessage = "{0}は必須です。")]
    // [StringLength(50, ErrorMessage = "{0}は{1}文字以内で指定してください。")]
    // [Display(Name = "タイトル")]
    [Required(ErrorMessage = "RequiredError")]
    [Display(Name = "Book_Title")]
    [XmlElement("Title")]
    public string Title { get; set; } = String.Empty;

    // [Range(10, 10000, ErrorMessage = "{0}は{1} ~ {2}の間で指定してください。")]
    // [DataType(DataType.Currency)]
    // [Display(Name = "価格")]
    [Range(10, 10000, ErrorMessage = "RangeError")]
    [Display(Name = "Book_Price")]
    [XmlElement("Price")]
    public int Price { get; set; }

    //[RegularExpression("翔泳社|技術評論社|SBクリエイティブ|日経BP|森北出版",
    //    ErrorMessage = "{0}は「{1}」のいずれかでなければなりません。")]
    // [InOptions("翔泳社,技術評論社,SBクリエイティブ,日経BP,森北出版")]
    // [Display(Name = "出版社")]
    [Required(ErrorMessage = "RequiredError")]
    [Display(Name = "Book_Publisher")]
    [XmlElement("Publisher")]
    public string Publisher { get; set; } = String.Empty;

    // [Range(typeof(DateTime), "2010-01-01", "2029-12-31",
    //     ErrorMessage = "{0}は{1:d} ~ {2:d}の範囲で指定してください。")]
    // [Display(Name = "刊行日")]
    // [DataType(DataType.Date)]
    [Required(ErrorMessage = "RequiredError")]
    [Display(Name = "Book_Published")]
    [XmlElement("Published")]
    public DateTime Published { get; set; }

    // [Display(Name = "配布サンプル")]
    [Display(Name = "Book_Sample")]
    [XmlElement("Sample")]
    public bool Sample { get; set; }

    [Timestamp]
    [XmlElement("RowVersion", DataType = "base64Binary")]
    public byte[]? RowVersion { get; set; }

    [XmlArray("Reviews")]
    [XmlArrayItem("Review")]
    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    [XmlArray("Authors")]
    [XmlArrayItem("Author")]
    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}

[XmlRoot("Books")]
public class BookList
{
    [XmlElement("Book")]
    public List<Book> Items { get; set; } = new();
}
