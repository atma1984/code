using Microsoft.Extensions.Logging; // ILoggerProvider, ILogger, LogLevel
using static System.Console;
namespace Packt.Shared;
public class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        // у нас могут быть разные реализации регистратора
        // для разных значений categoryName, но у нас есть только одна
        return new ConsoleLogger();
    }
    // если средство ведения журнала использует неуправляемые ресурсы,
    // то вы можете освободить их здесь
    public void Dispose() { }
}
public class ConsoleLogger : ILogger
{
    // если средство ведения журнала использует неуправляемые ресурсы,
    // то можно вернуть здесь класс, реализующий Idisposable
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }
    public bool IsEnabled(LogLevel logLevel)
    {
        // чтобы избежать переполнения журнала, можно выполнить фильтрацию
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Information:
            case LogLevel.None:
                return false;
            case LogLevel.Debug:
            case LogLevel.Warning:
            case LogLevel.Error:
            case LogLevel.Critical:
            default:
                return true;
        }
                ;
    }
    public void Log<TState>(LogLevel logLevel,
    EventId eventId, TState state, Exception? exception,
    Func<TState, Exception, string> formatter)
    {
        // пишем в журнал уровень и идентификатор события
        //Write($"Level: {logLevel}, Event Id: {eventId.Id}");
        //if (eventId.Id == 20100)
        //{
            // пишем в журнал уровень и идентификатор события
            Write("Level: {0}, Event Id: {1}, Event: {2}", logLevel, eventId.Id, eventId.Name);
            // выводим только существующее состояние или исключение
            if (state != null)
            {
                Write($", State: {state}");
            }
            if (exception != null)
            {
                Write($", Exception: {exception.Message}");
            }
            WriteLine();
        //}
    }
}