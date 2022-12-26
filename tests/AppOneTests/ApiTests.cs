using AppOne.Application;
using AppOne.Application.Models;
using FluentAssertions;

namespace AppOneTests;

public class ApiTests
{
    [Fact]
    public async Task Should_Get_Confirmation()
    {
        // arrange
        var api = new Api();
        var message = new Message();

        // act
        var confirmation = await api.SendAsync(message, CancellationToken.None);

        // assert
        confirmation.Should().NotBeNull();
    }
}