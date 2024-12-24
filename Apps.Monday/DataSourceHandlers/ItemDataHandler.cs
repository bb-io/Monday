using Apps.Monday.Actions;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class ItemDataHandler(InvocationContext invocationContext, 
    [ActionParameter] BoardIdentifier boardIdentifier) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(boardIdentifier.BoardId))
        {
            throw new Exception("Please provide Board ID first");
        }

        var actions = new ItemActions(InvocationContext);
        var response = await actions.SearchItemsAsync(boardIdentifier);

        return response.Items
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}