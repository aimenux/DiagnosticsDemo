using System.Diagnostics;
using AppOne.Application;
using AppOne.Application.Models;
using AppOne.Infrastructure.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var _ = DiagnosticListener.AllListeners.Subscribe(new ApiListener());

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddTransient<IApi, Api>())
    .Build();

var message = new Message
{
    Text = $"Hello {Guid.NewGuid():D}"
};

var api = host.Services.GetRequiredService<IApi>();
await api.SendAsync(message);

Console.WriteLine("Press any key to exit !");
Console.ReadKey();