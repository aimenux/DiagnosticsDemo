using System.Text;
using System.Text.Json;
using AppTwo.Application.Models;

namespace AppTwo.Application;

public class Api : IApi
{
    private readonly HttpClient _client;

    public Api(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<Confirmation> SendAsync(Message message, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(message);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(_client.BaseAddress, content, cancellationToken);
        var ok = response.IsSuccessStatusCode;
        var confirmation = new Confirmation
        {
            Status = ok ? "Sent" : "NotSent"
        };
        return confirmation;
    }
}