using Apps.Monday.Actions;
using Apps.Monday.Models.Requests;
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
            ItemId = "1753020441"
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
            //ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            InputText = $"Tests.Monday item {Guid.NewGuid()}",
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
            //ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            InputText = $"Tests.Monday item {Guid.NewGuid()}",
            GroupId = "group_title",
            ColumnIds = new[] { "date4", "status", "person" },
            ColumnValues = new[] { "2023-05-25", "Done", "vitalii.bezuhlyi@blackbird.io" }
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
            //ItemName = $"Tests.Monday item {Guid.NewGuid()}",
            InputText = $"Tests.Monday item {Guid.NewGuid()}",
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

    [TestMethod]
    public async Task UpdateItem_WithValidData_ShouldUpdateCustomField()
    {
        var actions = new ItemActions(InvocationContext);

        var updateRequest = new UpdateItemRequest
        {
            BoardId = "1753020434",
            ItemId = "1753020441",
            ColumnId = "dropdown_mkpd70c6",               
            Value = "New"
        };

        var updateResponse = await actions.UpdateItemAsync(updateRequest);
        updateResponse.Should().NotBeNull();
        updateResponse.Id.Should().Be("1753020441");

        Console.WriteLine("Updated Item:\n" + JsonConvert.SerializeObject(updateResponse, Formatting.Indented));
    }

    [TestMethod]
    public async Task AssignPersonToItem_WithValidData_ShouldAssignPerson()
    {
        var actions = new ItemActions(InvocationContext);

        var assignRequest = new AssignItemPersonRequest
        {
            BoardId = "1753020434",
            ItemId = "1755162632",
            ColumnId = "person",
            PersonId = "69964137"
        };

        var response = await actions.AssignPersonToItemAsync(assignRequest);
        response.Should().NotBeNull();
        response.Id.Should().Be("1755162632");

        Console.WriteLine("Assigned Person to Item:\n" + JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}