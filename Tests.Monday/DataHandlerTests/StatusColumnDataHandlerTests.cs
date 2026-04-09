using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using FluentAssertions;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class StatusColumnDataHandlerTests : DataHandlerTestBase<StatusColumnDataHandler>
{
    protected override StatusColumnDataHandler CreateHandler()
    {
        return new StatusColumnDataHandler(InvocationContext, new StatusColumnIdentifier
        {
            BoardId = BoardId
        });
    }

    protected override string GetSearchString() => "status";

    [TestMethod]
    public async Task Status_Column_ShouldReturnNotEmptyCollection()
    {
        var handler = CreateHandler();
        var dataSourceItems = await handler.GetDataAsync(new(), default);
        var sourceItems = dataSourceItems.ToList();

        sourceItems.Should().NotBeEmpty();
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }
}
