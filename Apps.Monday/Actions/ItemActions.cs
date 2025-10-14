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

[ActionList("Items")]
public class ItemActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search items", Description = "Retrieves all items from a specific board")]
    public async Task<SearchItemsResponse> SearchItemsAsync([ActionParameter] BoardIdentifier boardIdentifier)
    {
        var variables = new { ids = long.Parse(boardIdentifier.BoardId) };
        var request = new ApiRequest(GraphQlQueries.GetBoardWithItemsById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<BoardItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Boards.Any())
        {
            throw new PluginApplicationException($"Unable to find a board with the specified ID ({boardIdentifier.BoardId})");
        }

        var items = response.Data.Boards.First().ItemsPage.Items;
        return new()
        {
            Items = items,
            TotalCount = items.Count
        };
    }

    [Action("Get item", Description = "Retrieves an item by its specified ID")]
    public async Task<ItemResponse> GetItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new { ids = long.Parse(itemIdentifier.ItemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException($"Unable to find an item with the specified ID ({itemIdentifier.ItemId})");
        }

        return response.Data.Items.First();
    }

    [Action("Create item", Description = "Creates an item with the specified parameters")]
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
                throw new PluginMisconfigurationException("The number of Column IDs and Column values must be equal");
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
    
    [Action("Delete item", Description = "Deletes an item by its specified ID")]
    public async Task DeleteItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new
        {
            item_id = long.Parse(itemIdentifier.ItemId)
        };
        
        var request = new ApiRequest(GraphQlMutations.DeleteItem, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Archive  item", Description = "Archives an item by its specified ID")]
    public async Task ArchiveItemAsync([ActionParameter] ItemIdentifier itemIdentifier)
    {
        var variables = new
        {
            item_id = long.Parse(itemIdentifier.ItemId)
        };
        
        var request = new ApiRequest(GraphQlMutations.ArchiveItem, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update item", Description = "Updates a custom field for an item")]
    public async Task<ItemResponse> UpdateItemAsync([ActionParameter] UpdateItemRequest request)
    {
        string valueJson = TransformCustomFieldValue(request.ColumnId, request.Value);

        var variables = new
        {
            board_id = request.BoardId,
            item_id = request.ItemId,
            column_id = request.ColumnId,
            value = valueJson
        };

        var apiRequest = new ApiRequest(GraphQlMutations.UpdateItemField, variables, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(apiRequest);

        if (response?.Data == null || !response.Data.Items.Any())
        {
            return await GetItemAsync(new ItemIdentifier
            {
                BoardId = request.BoardId,
                ItemId = request.ItemId
            });
        }

        return response.Data.Items.First();
    }

    [Action("Assign person to item", Description = "Assigns a person to the specified item")]
    public async Task<ItemResponse> AssignPersonToItemAsync([ActionParameter] AssignItemPersonRequest request)
    {
        var valueJson = JsonConvert.SerializeObject(new
        {
            personsAndTeams = new[]
            {
                new
                {
                    id = long.Parse(request.PersonId),
                    kind = "person"
                }
            }
        });

        var variables = new
        {
            board_id = long.Parse(request.BoardId),
            item_id = long.Parse(request.ItemId),
            column_id = request.ColumnId,
            value = valueJson
        };

        var apiRequest = new ApiRequest(GraphQlMutations.UpdateItemField, variables, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<ChangeColumnValueDto>>(apiRequest);

        if (response?.Data == null || string.IsNullOrEmpty(response.Data.ChangeColumnValue.Id))
        {
            throw new PluginApplicationException($"Unable to assign person to item with ID {request.ItemId}");
        }

        return response.Data.ChangeColumnValue;
    }

    private string TransformCustomFieldValue(string columnId, string value)
    {
        if (columnId.StartsWith("status", StringComparison.OrdinalIgnoreCase))
        {
            return JsonConvert.SerializeObject(new { label = value });
        }

        if (columnId.StartsWith("dropdown", StringComparison.OrdinalIgnoreCase))
        {
            return JsonConvert.SerializeObject(new { labels = new string[] { value } });
        }

        if (columnId.StartsWith("name", StringComparison.OrdinalIgnoreCase))
        {
            return JsonConvert.SerializeObject($"{value}");
        }

        return JsonConvert.SerializeObject(new { text = value });
    }
}