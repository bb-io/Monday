using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Requests;
using Apps.Monday.Models.Responses;
using Apps.Monday.Models.Responses.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Monday.Actions;

[ActionList("Files")]
public class FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Add file to column", Description = "Adds a file to a specific file column of an item")]
    public async Task<AssetResponse> AddFileToColumnAsync(
        [ActionParameter] ItemIdentifier itemIdentifier,
        [ActionParameter] AddFileToColumnRequest addRequest)
    {
        var variables = new
        {
            item_id = itemIdentifier.ItemId,
            column_id = addRequest.ColumnId
        };

        var stream = await fileManagementClient.DownloadAsync(addRequest.File);
        var bytes = await stream.GetByteData();

        var map = new { file = "variables.file" };
        var request = new ApiRequest("/file", GraphQlMutations.AddFileToColumn, variables, map, Creds)
            .AddFile("file", bytes, addRequest.File.Name);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<AddFileToColumnResponse>>(request);
        return response.Data.AddFileToColumn;
    }
}