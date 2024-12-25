using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Requests;
using Apps.Monday.Models.Responses.Boards;
using Apps.Monday.Models.Responses.Items;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apps.Monday.Actions;

[ActionList]
public class ItemActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search items", Description = "Retrieves all items from specific board")]
    public async Task<SearchItemsResponse> SearchItemsAsync([ActionParameter] BoardIdentifier boardIdentifier)
    {
        var variables = new { ids = int.Parse(boardIdentifier.BoardId) };
        var request = new ApiRequest(GraphQlQueries.GetBoardWithItemsById, variables, Creds);

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
    public async Task<ItemResponse> GetItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new { ids = int.Parse(itemIdentifier.ItemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException("Couldn't find board by specified ID");
        }

        return response.Data.Items.First();
    }

    [Action("Create item", Description = "Create item with specified parameters")]
    public async Task<ItemResponse> CreateItemAsync([ActionParameter] CreateItemRequest createItemRequest)
    {
        var variables = new Dictionary<string, string>
        {
            { "board_id", createItemRequest.BoardId },
            { "item_name", createItemRequest.ItemName }
        };

        if (!string.IsNullOrEmpty(createItemRequest.GroupId))
        {
            variables.Add("group_id", createItemRequest.GroupId);
        }

        if (createItemRequest.ColumnIds != null && createItemRequest.ColumnValues != null)
        {
            if (createItemRequest.ColumnIds.Count() != createItemRequest.ColumnValues.Count())
            {
                throw new PluginMisconfigurationException("Column IDs and Column values count should be equal");
            }

            var zippedColumns = createItemRequest.ColumnIds.Zip(createItemRequest.ColumnValues).ToList();

            var jObject = new JObject();
            foreach (var zippedColumn in zippedColumns)
            {
                jObject.Add(zippedColumn.First, new JValue(zippedColumn.Second));
            }

            var json = JsonConvert.SerializeObject(jObject);
            variables.Add("column_values", json);
        }
        
        var request = new ApiRequest(GraphQlMutations.CreateItem, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<CreateItemResponse>>(request);
        return response.Data.CreateItem;
    }
    
    [Action("Delete item", Description = "Deletes an item based on specified ID")]
    public async Task DeleteItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new
        {
            item_id = int.Parse(itemIdentifier.ItemId)
        };
        
        var request = new ApiRequest(GraphQlMutations.DeleteItem, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Archive  item", Description = "Archive an item based on specified ID")]
    public async Task ArchiveItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new
        {
            item_id = int.Parse(itemIdentifier.ItemId)
        };
        
        var request = new ApiRequest(GraphQlMutations.ArchiveItem, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
}