using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Requests;
using Apps.Monday.Models.Responses;
using Apps.Monday.Models.Responses.Boards;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Actions;

[ActionList]
public class BoardActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search boards", Description = "Retrieves all boards filtered by the specified parameters")]
    public async Task<SearchBoardsResponse> SearchBoardsAsync([ActionParameter] SearchBoardRequest searchBoardRequest)
    {
        return await Client.PaginateAsync<SearchBoardsResponse, BoardResponse>(
            GraphQlQueries.GetBoards,
            limit: 10  
        );
    }

    [Action("Get board", Description = "Retrieves a board by its specified ID")]
    public async Task<BoardResponse> GetBoardAsync([ActionParameter] BoardIdentifier boardIdentifier)
    {
        var variables = new { ids = long.Parse(boardIdentifier.BoardId) };
        var request = new ApiRequest(GraphQlQueries.GetBoardById, variables, Creds);
            
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchBoardsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException($"Unable to find a board with the specified ID ({boardIdentifier.BoardId})");
        }

        return response.Data.Items.First();
    }
}