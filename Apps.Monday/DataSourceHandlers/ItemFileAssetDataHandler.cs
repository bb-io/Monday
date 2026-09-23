using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Responses.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class ItemFileAssetDataHandler : AppInvocable, IAsyncDataSourceItemHandler
{
    private readonly long _itemId;

    public ItemFileAssetDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] ItemIdentifier itemIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrWhiteSpace(itemIdentifier.ItemId))
            throw new PluginMisconfigurationException("Please specify an Item ID first");

        if (!long.TryParse(itemIdentifier.ItemId, out long itemId))
            throw new PluginMisconfigurationException("Item ID should be a number");
        
        _itemId = itemId;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var variables = new { ids = _itemId };
        var request = new ApiRequest(GraphQlQueries.GetItemAssetsById, variables, Creds);
        
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemFilesResponse>>(request);
        return response.Data.Items
            .SelectMany(x => x.Assets)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString))
            .Select(x => new DataSourceItem(x.Id, x.Name)).ToList();
    }
}