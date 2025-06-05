using System;

namespace SelfAspNet.Models;

public class IRecordableTimestamp
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
