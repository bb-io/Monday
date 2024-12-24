using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Responses.Boards;
using Apps.Monday.Models.Responses.Items;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Actions;

[ActionList]
public class ItemActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search items", Description = "Retrieves all items from specific board")]
    public async Task<SearchItemsResponse> SearchItemsAsync([ActionParameter] BoardIdentifier boardIdentifier)
    {
        var variables = new { ids = int.Parse(boardIdentifier.BoardId) };
        var request = new ApiRequest(GraphQlConstants.GetBoardWithItemsById, variables, Creds);
            
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<BoardItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Boards.Any())
        {
            throw new PluginApplicationException("Couldn't find board by specified ID");
        }

        var items = response.Data.Boards.First().ItemsPage.Items;
        return new()
        {
            Items = items,
            TotalCount = items.Count
        };
    }
    
    [Action("Get item", Description = "Get item based on specified ID")]
    public async Task<ItemResponse> GetItemAsync([ActionParameter] ItemIdentifier boardIdentifier)
    {
        var variables = new { ids = int.Parse(boardIdentifier.ItemId) };
        var request = new ApiRequest(GraphQlConstants.GetItemById, variables, Creds);
            
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException("Couldn't find board by specified ID");
        }

        return response.Data.Items.First();
    }
}