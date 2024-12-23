using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Requests;
using Apps.Monday.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Actions;

[ActionList]
public class BoardActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Search boards", Description = "Returns all board filtered by specified parameters")]
    public async Task<SearchBoardsResponse> SearchBoardsAsync([ActionParameter] SearchBoardRequest searchBoardRequest)
    {
        return await Client.PaginateAsync<SearchBoardsResponse, BoardResponse>(
            GraphQlConstants.GetBoards,
            limit: 2  
        );
    }
}