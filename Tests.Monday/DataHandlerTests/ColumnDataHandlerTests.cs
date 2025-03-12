using Apps.Monday.DataSourceHandlers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class ColumnDataHandlerTests : DataHandlerTestBase<ColumnDataHandler>
{
    protected override ColumnDataHandler CreateHandler()
    {
        return new ColumnDataHandler(InvocationContext, new() { BoardId = BoardId });
    }

    protected override string GetSearchString() => "Date";
}