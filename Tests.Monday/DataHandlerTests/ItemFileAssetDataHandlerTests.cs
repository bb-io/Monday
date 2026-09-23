using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class ItemFileAssetDataHandlerTests : DataHandlerTestBase<ItemFileAssetDataHandler>
{
    protected override ItemFileAssetDataHandler CreateHandler()
    {
        var item = new ItemIdentifier
        {
            BoardId = BoardId,
            ItemId = "3237187394"
        };
        return new ItemFileAssetDataHandler(InvocationContext, item);
    }

    protected override string GetSearchString() => "mp3";
}