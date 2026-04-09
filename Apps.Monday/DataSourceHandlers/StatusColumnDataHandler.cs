using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class StatusColumnDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] BoardIdentifier boardIdentifier)
    : ColumnDataHandler(invocationContext, boardIdentifier)
{
    protected override List<string>? ColumnTypes => ["color"];
}
