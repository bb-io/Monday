using Apps.Monday.Helpers;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class UpdateDataSource(InvocationContext invocationContext, 
    [ActionParameter] ItemIdentifier itemIdentifier) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(itemIdentifier.ItemId))
            throw new PluginMisconfigurationException("Please provide 'Item ID' first");
        
        var helper = new ItemHelper(InvocationContext);
        var item = await helper.GetItem(itemIdentifier.ItemId);

        return item.Updates
            .Where(x => context.SearchString == null ||
                        x.TextBody.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.TextBody));
    }
}