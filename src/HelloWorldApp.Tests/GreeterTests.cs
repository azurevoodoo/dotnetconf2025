using HelloWorldApp.Common;

namespace HelloWorldApp.Tests;

public class GreeterTests
{
    [Fact]
    public void TheGreetMethod()
    {
        // Given
        var greeter = new Greeter();

        // When
        var result = greeter.Greet();

        // Then
        Assert.Equal("Hello World!", result);
    }
}