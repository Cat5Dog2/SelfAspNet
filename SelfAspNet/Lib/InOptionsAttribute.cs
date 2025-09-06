using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SelfAspNet.Lib;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class InOptionsAttribute : ValidationAttribute
{
    private string _options;

    public InOptionsAttribute(string options)
    {
        _options = options;
        ErrorMessage = "{0}は「{1}」のいずれかの値で指定します。";
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture,
            ErrorMessageString, name, _options);
    }

    public override bool IsValid(object? value)
    {
        var v = value as string;
        if (string.IsNullOrEmpty(v)) { return true; }
        if (_options.Split(",").Any(opt => opt.Trim() == v))
        {
            return true;
        }
        return false;
    }
}
