using System.Diagnostics;
using AppOne.Infrastructure.Logging;

namespace AppOne.Infrastructure.Diagnostics;

public sealed class ApiListener : IObserver<DiagnosticListener>
{
    private static readonly object Locker = new ();

    private readonly IConsoleLogger _logger;
    private readonly ICollection<IDisposable> _listeners = new List<IDisposable>();

    public ApiListener(IConsoleLogger logger = null)
    {
        _logger = logger ?? new ConsoleLogger();
    }

    public void OnNext(DiagnosticListener listener)
    {
        lock (Locker)
        {
            if (listener.Name == Constants.ApiDiagnosticListenerName)
            {
                var subscription = listener.Subscribe(new ApiInterceptor());
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