using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SelfAspNet.Lib;

public class FileLogger : ILogger
{
    private readonly object _lockobj = new();
    private readonly string _filepath;
    private readonly string _category;

    public FileLogger(string filepath, string category)
    {
        _filepath = filepath;
        _category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lockobj)
        {
            var path = Path.Combine(_filepath, $"core_{DateTime.Now.ToString("yyyy-MM-dd")}.log");
            var except = exception == null ? "" :
                $"\n{exception.GetType()}:{exception.Message}\n{exception.StackTrace}";
            File.AppendAllText(path,
                $"{logLevel} - {_category} -（{eventId}） {DateTime.Now.ToString()} \n" +
                $"{formatter(state, exception)}{except}\n"
            );
        }
    }
}
