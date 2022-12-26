using AppOne.Application.Models;

namespace AppOne.Application;

public interface IApi
{
    Task<Confirmation> SendAsync(Message message, CancellationToken cancellationToken = default);
}