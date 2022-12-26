using System.Diagnostics;
using AppOne.Application.Models;
using AppOne.Infrastructure.Diagnostics;

namespace AppOne.Application;

public class Api : IApi
{
    private static readonly DiagnosticSource Source = new DiagnosticListener(Constants.ApiDiagnosticListenerName);
    
    public Task<Confirmation> SendAsync(Message message, CancellationToken cancellationToken)
    {
        var ok = !string.IsNullOrWhiteSpace(message.Text);
        var confirmation = new Confirmation
        {
            Status = ok ? "Sent" : "NotSent"
        };
        
        if (Source.IsEnabled(Constants.ApiDiagnosticEventName))
        {
            var obj = new
            {
                Text = message.Text, 
                Status = confirmation.Status
            };
            
            Source.Write(Constants.ApiDiagnosticEventName, obj);
        }
        
        return Task.FromResult(confirmation);
    }
}