using AppTwo.Application;
using AppTwo.Application.Models;
using FluentAssertions;

namespace AppTwoTests;

public class ApiTests
{
    [Fact]
    public async Task Should_Get_Confirmation()
    {
        // arrange
        var client = new HttpClient(new FakeHttpMessageHandler())
        {
            BaseAddress = new Uri("https://tests")
        };
        var api = new Api(client);
        var message = new Message();

        // act
        var confirmation = await api.SendAsync(message, CancellationToken.None);

        // assert
        confirmation.Should().NotBeNull();
    }
}