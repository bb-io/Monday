using Apps.Monday.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using FluentAssertions;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class ConnectionValidatorTests : TestBase
{
    [TestMethod]
    public async Task ValidatesCorrectConnection()
    {
        var validator = new ConnectionValidator();

        var result = await validator.ValidateConnection(Creds, CancellationToken.None);
        result.IsValid.Should().Be(true);
        Console.WriteLine(result.Message);
    }

    [TestMethod]
    public async Task DoesNotValidateIncorrectConnection()
    {
        var validator = new ConnectionValidator();

        var newCredentials = Creds.Select(x => new AuthenticationCredentialsProvider(x.KeyName, x.Value + "_incorrect"));
        var result = await validator.ValidateConnection(newCredentials, CancellationToken.None);
        result.IsValid.Should().Be(false);
        Console.WriteLine(result.Message);
    }
}