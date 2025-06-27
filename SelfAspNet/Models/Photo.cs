using System;

namespace SelfAspNet.Models;

public class Photo
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string ContentType { get; set; } = String.Empty;
    public byte[] Content { get; set; } = null!;
}
