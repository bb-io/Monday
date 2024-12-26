using Apps.Monday.Actions;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class UpdateDataSource(InvocationContext invocationContext, 
    [ActionParameter] ItemIdentifier itemIdentifier) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(itemIdentifier.ItemId))
        {
            throw new Exception("Please provide 'Item ID' first");
        }

        var actions = new ItemActions(InvocationContext);
        var response = await actions.GetItemAsync(itemIdentifier);

        return response.Updates
            .Where(x => context.SearchString == null ||
                        x.TextBody.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.TextBody));
    }
}