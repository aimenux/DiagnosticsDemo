namespace AppOne.Infrastructure.Logging;

public class ConsoleLogger : IConsoleLogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}