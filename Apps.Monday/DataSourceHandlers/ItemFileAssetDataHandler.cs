using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Responses.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class ItemFileAssetDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] ItemIdentifier itemIdentifier) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var variables = new { ids = long.Parse(itemIdentifier.ItemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemAssetsById, variables, Creds);
        
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemFilesResponse>>(request);
        return response.Data.Items
            .SelectMany(x => x.Assets)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString))
            .Select(x => new DataSourceItem(x.Id, x.Name)).ToList();
    }
}