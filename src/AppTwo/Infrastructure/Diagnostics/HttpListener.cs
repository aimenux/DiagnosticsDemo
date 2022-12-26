using System.Diagnostics;
using AppTwo.Infrastructure.Logging;

namespace AppTwo.Infrastructure.Diagnostics;

public sealed class HttpListener : IObserver<DiagnosticListener>
{
    private static readonly object Locker = new ();

    private readonly IConsoleLogger _logger;
    private readonly ICollection<IDisposable> _listeners = new List<IDisposable>();

    public HttpListener(IConsoleLogger logger = null)
    {
        _logger = logger ?? new ConsoleLogger();
    }

    public void OnNext(DiagnosticListener listener)
    {
        lock (Locker)
        {
            if (listener.Name == Constants.HttpDiagnosticListenerName)
            {
                var subscription = listener.Subscribe(new HttpInterceptor());
                _listeners.Add(subscription);
            }
        }
    }
    
    public void OnCompleted()
    {
        lock(_listeners)
        {
            foreach (var listener in _listeners)
            {
                listener.Dispose();
            }
            _listeners.Clear();
        }
    }

    public void OnError(Exception error)
    {
        _logger.Log($"An error has occured {error}");
    }
}