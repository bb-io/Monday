using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Tests.Monday.Base;

namespace Tests.Monday.DataHandlerTests;

[TestClass]
public class PersonColumnDataHandlerTests : DataHandlerTestBase<PersonColumnDataHandler>
{
    protected override PersonColumnDataHandler CreateHandler()
    {
        return new PersonColumnDataHandler(InvocationContext, new BoardIdentifier { BoardId = BoardId });
    }

    protected override string GetSearchString() => "Person";
}