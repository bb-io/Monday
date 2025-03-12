using Apps.Monday.DataSourceHandlers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class ItemDataHandlerTests : DataHandlerTestBase<ItemDataHandler>
{
    protected override ItemDataHandler CreateHandler()
    {
        return new ItemDataHandler(InvocationContext, new() { BoardId = BoardId });
    }

    protected override string GetSearchString() => "new";
}