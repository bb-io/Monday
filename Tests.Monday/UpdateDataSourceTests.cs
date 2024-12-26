using Apps.Monday.DataSourceHandlers;
using FluentAssertions;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class UpdateDataSourceTests : TestBase
{
    [TestMethod]
    public async Task GetDataAsync_WithoutFilters_ShouldReturnNotEmptyCollection()
    {
        var updateDataSource = new UpdateDataSource(InvocationContext, new()
        {
            BoardId = BoardId,
            ItemId = ItemId
        });
        var dataSourceItems = await updateDataSource.GetDataAsync(new(), default);
        var sourceItems = dataSourceItems.ToList();
        
        sourceItems.Should().NotBeEmpty();
        Console.WriteLine(sourceItems.Count);
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }
    
    [TestMethod]
    public async Task GetDataAsync_WithSearchString_ShouldReturnNotEmptyCollection()
    {
        var searchString = "Postman";
        
        var updateDataSource = new UpdateDataSource(InvocationContext, new()
        {
            BoardId = BoardId,
            ItemId = ItemId
        });
        var dataSourceItems = await updateDataSource.GetDataAsync(new()
        {
            SearchString = searchString
        }, default);
        var sourceItems = dataSourceItems.ToList();
        
        sourceItems.Should().NotBeEmpty();
        sourceItems.ForEach(x => x.DisplayName.Should().Contain(searchString));

        Console.WriteLine(sourceItems.Count);
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }
}