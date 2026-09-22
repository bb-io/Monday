using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class FileColumnDataHandlerTests : DataHandlerTestBase<FileColumnDataHandler>
{
    protected override FileColumnDataHandler CreateHandler()
    {
        return new FileColumnDataHandler(InvocationContext, new BoardIdentifier { BoardId = BoardId });
    }

    protected override string GetSearchString() => "";
}