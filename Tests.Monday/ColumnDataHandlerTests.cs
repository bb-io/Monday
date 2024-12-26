using Apps.Monday.DataSourceHandlers;
using FluentAssertions;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class ColumnDataHandlerTests : TestBase
{
    [TestMethod]
    public async Task GetDataAsync_WithoutFilters_ShouldReturnNotEmptyCollection()
    {
        var columnDataHandler = new ColumnDataHandler(InvocationContext, new()
        {
            BoardId = BoardId
        });
        
        var dataSourceItems = await columnDataHandler.GetDataAsync(new(), default);
        var sourceItems = dataSourceItems.ToList();
        
        sourceItems.Should().NotBeEmpty();
        Console.WriteLine(sourceItems.Count);
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }
    
    [TestMethod]
    public async Task GetDataAsync_WithSearchString_ShouldReturnNotEmptyCollection()
    {
        var searchString = "Date";
        
        var columnDataHandler = new ColumnDataHandler(InvocationContext, new()
        {
            BoardId = BoardId
        });
        var dataSourceItems = await columnDataHandler.GetDataAsync(new()
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