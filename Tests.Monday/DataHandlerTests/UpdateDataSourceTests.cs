using Apps.Monday.DataSourceHandlers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class UpdateDataSourceTests : DataHandlerTestBase<UpdateDataSource>
{
    protected override UpdateDataSource CreateHandler()
    {
        return new UpdateDataSource(InvocationContext, new()
        {
            BoardId = BoardId,
            ItemId = ItemId
        });
    }

    protected override string GetSearchString() => "Postman";
}