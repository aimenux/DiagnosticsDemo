using AppTwo.Infrastructure.Logging;

namespace AppTwo.Infrastructure.Diagnostics;

public sealed class HttpInterceptor : IObserver<KeyValuePair<string, object>>
{
    private readonly IConsoleLogger _logger;

    public HttpInterceptor(IConsoleLogger logger = null)
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
        if (keyValue.Key == Constants.HttpDiagnosticEventName1)
        {
            _logger.Log($"Request: {keyValue.Value}");
        }
        
        if (keyValue.Key == Constants.HttpDiagnosticEventName2)
        {
            _logger.Log($"Response: {keyValue.Value}");
        }
    }
}