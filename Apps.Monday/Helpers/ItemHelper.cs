using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses.Items;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Helpers;

public class ItemHelper(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    public async Task<ItemResponse> GetItem(string itemId)
    {
        var variables = new { ids = long.Parse(itemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response.Data == null || response.Data.Items.Count == 0)
            throw new PluginApplicationException($"Unable to find an item with the specified ID ({itemId})");

        return response.Data.Items.First();
    }
}