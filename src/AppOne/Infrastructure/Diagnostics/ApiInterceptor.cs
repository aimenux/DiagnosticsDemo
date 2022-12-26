using AppOne.Infrastructure.Logging;

namespace AppOne.Infrastructure.Diagnostics;

public sealed class ApiInterceptor : IObserver<KeyValuePair<string, object>>
{
    private readonly IConsoleLogger _logger;

    public ApiInterceptor(IConsoleLogger logger = null)
    {
        _logger = logger ?? new ConsoleLogger();
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
        _logger.Log($"An error has occured {error}");
    }

    public void OnNext(KeyValuePair<string, object> keyValue)
    {
        _logger.Log($"[{DateTime.Now:G}] Key: {keyValue.Key}");
        _logger.Log($"[{DateTime.Now:G}] Value: {keyValue.Value}");
    }
}