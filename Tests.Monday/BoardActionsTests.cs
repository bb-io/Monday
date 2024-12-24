using Apps.Monday.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using FluentAssertions;
using Newtonsoft.Json;
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
    
    [TestMethod]
    public async Task GetBoard_BoardExists_ShouldReturnBoard()
    {
        var boardId = "1753020434";
        
        var actions = new BoardActions(InvocationContext);
        var board = await actions.GetBoardAsync(new() { BoardId = boardId });

        board.Id.Should().Be(boardId);
        Console.WriteLine(JsonConvert.SerializeObject(board, Formatting.Indented));
    }
    
    [TestMethod]
    public async Task GetBoard_BoardDoesNotExists_ShouldThrowException()
    {
        var boardId = "1111";
        
        var actions = new BoardActions(InvocationContext);
        await Assert.ThrowsExceptionAsync<PluginApplicationException>(async () =>
            await actions.GetBoardAsync(new() { BoardId = boardId }));
    }
}