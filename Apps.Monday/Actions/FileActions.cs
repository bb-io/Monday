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
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Monday.Actions;

[ActionList("Files")]
public class FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : AppInvocable(invocationContext)
{
    [Action("Download file", Description = "Download a specific file from an item")]
    public async Task<DownloadFileResponse> DownloadFile(
        [ActionParameter] ItemIdentifier itemIdentifier,
        [ActionParameter] DownloadFileRequest downloadRequest)
    {
        string itemId = itemIdentifier.ItemId;
        string fileId = downloadRequest.FileId;
        
        var variables = new { ids = long.Parse(itemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemAssetsById, variables, Creds);
        
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemFilesResponse>>(request);
        var file = response.Data.Items.SelectMany(x => x.Assets).FirstOrDefault(x => x.Id == fileId) ?? 
                   throw new PluginMisconfigurationException($"File ID {fileId} was not found for item ID {itemId}");

        using var restClient = new RestClient();
        await using var networkStream = await restClient.DownloadStreamAsync(new RestRequest(file.PublicUrl))
                                        ?? throw new PluginApplicationException($"Failed to download the file '{file.Name}'");

        var seekableStream = new MemoryStream();
        await networkStream.CopyToAsync(seekableStream);
        seekableStream.Position = 0;
        
        var fileReference = await fileManagementClient.UploadAsync(seekableStream, MimeTypes.GetMimeType(file.Name), file.Name);
        return new(fileReference);
    }
    
    [Action("Add file to column", Description = "Adds a file to a specific file column of an item")]
    public async Task<AssetResponse> AddFileToColumn(
        [ActionParameter] ItemIdentifier itemIdentifier,
        [ActionParameter] AddFileToColumnRequest addRequest)
    {
        var variables = new
        {
            item_id = itemIdentifier.ItemId,
            column_id = addRequest.ColumnId
        };

        await using var stream = await fileManagementClient.DownloadAsync(addRequest.File);
        var bytes = await stream.GetByteData();

        var map = new { file = "variables.file" };
        var request = new ApiRequest("/file", GraphQlMutations.AddFileToColumn, variables, map, Creds)
            .AddFile("file", bytes, addRequest.File.Name);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<AddFileToColumnResponse>>(request);
        return response.Data.AddFileToColumn;
    }
}