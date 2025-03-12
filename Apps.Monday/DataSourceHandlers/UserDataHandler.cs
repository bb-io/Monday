using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses.Users;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class UserDataHandler(InvocationContext invocationContext) 
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new ApiRequest(GraphQlQueries.GetUsers, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<UsersResponse>>(request);

        return response.Data.Users
            .Where(x => context.SearchString == null ||
                       x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}
