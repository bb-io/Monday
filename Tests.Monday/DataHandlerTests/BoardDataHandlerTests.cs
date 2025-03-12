using Apps.Monday.DataSourceHandlers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class BoardDataHandlerTests : DataHandlerTestBase<BoardDataHandler>
{
    protected override BoardDataHandler CreateHandler()
    {
        return new BoardDataHandler(InvocationContext);
    }

    protected override string GetSearchString() => "Blackbird";
}