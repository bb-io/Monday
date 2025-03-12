using Apps.Monday.DataSourceHandlers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class UserDataHandlerTests : DataHandlerTestBase<UserDataHandler>
{
    protected override UserDataHandler CreateHandler()
    {
        return new UserDataHandler(InvocationContext);
    }

    protected override string GetSearchString() => "Vitalii";
}
