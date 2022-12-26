using AppTwo.Application.Models;

namespace AppTwo.Application;

public interface IApi
{
    Task<Confirmation> SendAsync(Message message, CancellationToken cancellationToken = default);
}