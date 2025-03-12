using Blackbird.Applications.Sdk.Common.Dynamic;
using FluentAssertions;

namespace Tests.Monday.Base;

public abstract class DataHandlerTestBase<T> : TestBase where T : IAsyncDataSourceItemHandler
{
    protected abstract T CreateHandler();
    
    [TestMethod]
    public async Task GetDataAsync_WithoutFilters_ShouldReturnNotEmptyCollection()
    {
        var handler = CreateHandler();
        var dataSourceItems = await handler.GetDataAsync(new(), default);
        var sourceItems = dataSourceItems.ToList();
        
        sourceItems.Should().NotBeEmpty();
        Console.WriteLine(sourceItems.Count);
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }
    
    [TestMethod]
    public async Task GetDataAsync_WithSearchString_ShouldReturnNotEmptyCollection()
    {
        var searchString = GetSearchString();
        
        var handler = CreateHandler();
        var dataSourceItems = await handler.GetDataAsync(new()
        {
            SearchString = searchString
        }, default);
        var sourceItems = dataSourceItems.ToList();
        
        sourceItems.Should().NotBeEmpty();
        sourceItems.ForEach(x => x.DisplayName.Should().Contain(searchString));

        Console.WriteLine(sourceItems.Count);
        sourceItems.ForEach(x => Console.WriteLine($"{x.Value}: {x.DisplayName}"));
    }

    protected abstract string GetSearchString();
}
