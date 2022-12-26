using System.Diagnostics;
using AppTwo.Application;
using AppTwo.Application.Models;
using AppTwo.Infrastructure.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var _ = DiagnosticListener.AllListeners.Subscribe(new HttpListener());

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddHttpClient<IApi, Api>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var webHookUrl = configuration.GetValue<string>("Settings:WebHookUrl");
            client.BaseAddress = new Uri(webHookUrl);
        }))
    .Build();

var message = new Message
{
    Text = $"Hello {Guid.NewGuid():D}"
};

var api = host.Services.GetRequiredService<IApi>();
await api.SendAsync(message);

Console.WriteLine("Press any key to exit !");
Console.ReadKey();