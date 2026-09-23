using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses;
using Apps.Monday.Models.Responses.Boards;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;

namespace Apps.Monday.Helpers;

public class BoardHelper(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    public async Task<string> GetSubitemsBoardId(string parentBoardId)
    {
        var variables = new { ids = long.Parse(parentBoardId) };
        var request = new ApiRequest(GraphQlQueries.GetSubitemsColumnSettings, variables, Creds);
        
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchBoardsResponse>>(request);
        string? settingsStr = response.Data.Items.FirstOrDefault()?.Columns.FirstOrDefault()?.SettingsStr;

        if (string.IsNullOrWhiteSpace(settingsStr))
        {
            throw new PluginMisconfigurationException(
                $"Board {parentBoardId} has no subitems column. " +
                $"Add at least one subitem to the board first.");
        }

        return JsonConvert.DeserializeObject<SettingsStrResponse>(settingsStr)?.BoardIds.FirstOrDefault()
               ?? throw new PluginApplicationException($"Unable to resolve the subitems board of board {parentBoardId}");
    }
}