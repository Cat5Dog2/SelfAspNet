using System;

namespace SelfAspNet.Lib;

public interface IMyService1
{
    Guid Id { get; }
}

public interface IMyService2
{
    Guid Id { get; }
}

public interface IMyService3
{
    Guid Id { get; }
}

public class MyService : IMyService1, IMyService2, IMyService3
{
    private readonly Guid _id;
    public MyService()
    {
        _id = Guid.NewGuid();
    }
    public Guid Id => _id;
}
