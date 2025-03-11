using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Responses.Boards;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class ColumnDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] BoardIdentifier boardIdentifier) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(boardIdentifier.BoardId))
        {
            throw new Exception("Please provide Board ID first");
        }

        var variables = new { ids = long.Parse(boardIdentifier.BoardId) };
        var request = new ApiRequest(GraphQlQueries.GetBoardColumns, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchBoardsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException("Couldn't find board by specified ID");
        }

        return response.Data.Items.First()
            .Columns
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Title.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, $"[{x.Type}] {x.Title}"));
    }
}