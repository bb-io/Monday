using Apps.Monday.Actions;
using FluentAssertions;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class BoardActionsTests : TestBase
{
    [TestMethod]
    public async Task SearchBoards_WithoutFilters_ShouldReturnNotEmptyCollection()
    {
        var actions = new BoardActions(InvocationContext);
        var response = await actions.SearchBoardsAsync(new());

        response.TotalCount.Should().BeGreaterThan(0);
        response.TotalCount.Should().Be(response.Items.Count);
        response.Items.Should().NotBeEmpty();
        
        Console.WriteLine(response.TotalCount);
        response.Items.ForEach(x => Console.WriteLine($"{x.Id}: {x.Name}"));
    }
}