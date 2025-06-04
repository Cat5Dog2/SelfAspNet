using System;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SelfAspNet.Helpers;

[HtmlTargetElement(Attributes = "asp-highlight")]
public class HighlightTagHelper : TagHelper
{
    [HtmlAttributeName("asp-highlight")]
    public string? BackgroundColor { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("style", $"background-color: {BackgroundColor ?? "#ff0"}");
    }
}
