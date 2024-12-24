using Apps.Monday.Actions;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class ItemActionsTests : TestBase
{
    [TestMethod]
    public async Task SearchItems_WithoutFilters_ShouldReturnNotEmptyCollection()
    {
        var actions = new ItemActions(InvocationContext);
        var response = await actions.SearchItemsAsync(new()
        {
            BoardId = BoardId
        });

        response.TotalCount.Should().BeGreaterThan(0);
        response.TotalCount.Should().Be(response.Items.Count);
        response.Items.Should().NotBeEmpty();
        
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
    
    [TestMethod]
    public async Task GetItem_WithValidId_ShouldReturnItem()
    {
        var actions = new ItemActions(InvocationContext);
        var response = await actions.GetItemAsync(new()
        {
            BoardId = BoardId,
            ItemId = ItemId
        });

        response.Id.Should().NotBeEmpty();
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}