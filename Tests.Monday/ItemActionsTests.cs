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
    
    [TestMethod]
    public async Task CreateAndDeleteItem_WithValidData_ShouldReturnItem()
    {
        var actions = new ItemActions(InvocationContext);
        var response = await actions.CreateItemAsync(new()
        {
            BoardId = BoardId,
            ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            GroupId = "group_title"
        });

        response.Id.Should().NotBeEmpty();
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

        await actions.DeleteItemAsync(new()
        {
            BoardId = BoardId,
            ItemId = response.Id
        });
        
        Console.WriteLine("\nSuccessfully deleted newly created item");
    }
    
    [TestMethod]
    public async Task CreateAndDeleteItem_WithValidColumns_ShouldReturnItem()
    {
        var actions = new ItemActions(InvocationContext);
        var response = await actions.CreateItemAsync(new()
        {
            BoardId = BoardId,
            ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            GroupId = "group_title",
            ColumnIds = new []{"date4", "status", "person"},
            ColumnValues = new []{"2023-05-25", "Done", "vitalii.bezuhlyi@blackbird.io"}
        });

        response.Id.Should().NotBeEmpty();
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

        await actions.DeleteItemAsync(new()
        {
            BoardId = BoardId,
            ItemId = response.Id
        });
        
        Console.WriteLine("\nSuccessfully deleted newly created item");
    }
    
    [TestMethod]
    public async Task CreateAndArchiveItem_WithValidData_ShouldArchive()
    {
        var actions = new ItemActions(InvocationContext);
        var response = await actions.CreateItemAsync(new()
        {
            BoardId = BoardId,
            ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            GroupId = "group_title"
        });

        response.Id.Should().NotBeEmpty();
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

        await actions.ArchiveItemAsync(new()
        {
            BoardId = BoardId,
            ItemId = response.Id
        });
        
        Console.WriteLine("\nSuccessfully archived newly created item");
    }
}