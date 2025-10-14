using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Requests;
using Apps.Monday.Models.Responses.Updates;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Monday.Actions;

[ActionList("Updates")]
public class UpdateActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Get update", Description = "Retrieves an update based on the specified Item ID")] 
    public async Task<UpdateResponse> GetUpdateAsync([ActionParameter] UpdateIdentifier updateIdentifier)
    {
        var itemActions = new ItemActions(InvocationContext);
        var item = await itemActions.GetItemAsync(updateIdentifier);

        var specificUpdate = item.Updates.FirstOrDefault(x => x.Id == updateIdentifier.UpdateId)
                             ?? throw new PluginApplicationException(
                                 $"Unable to find an update with the specified ID ({updateIdentifier.UpdateId}) in the item with ID {updateIdentifier.ItemId}");

        return specificUpdate;
    }
    
    [Action("Add update", Description = "Creates an update (comment) in a specific item based on its ID")]
    public async Task<UpdateResponse> AddUpdateAsync([ActionParameter] CreateUpdateRequest createUpdateRequest)
    {
        var variables = new
        {
            item_id = createUpdateRequest.ItemId,
            body = createUpdateRequest.Body
        };
        
        var request = new ApiRequest(GraphQlMutations.CreateUpdate, variables, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<CreateUpdateResponse>>(request);
        return response.Data.CreateUpdate;
    }
    
    [Action("Add attachment", Description = "Adds an attachment (file) to an item, optionally to a specific update")]
    public async Task<UpdateResponse> AddAttachmentAsync([ActionParameter] AddAttachmentRequest addAttachmentRequest)
    {
        var updateId = addAttachmentRequest.UpdateId;
        if (string.IsNullOrEmpty(updateId))
        {
            var createResponse = await AddUpdateAsync(new CreateUpdateRequest 
            { 
                ItemId = addAttachmentRequest.ItemId,
                Body = $"File uploaded: {addAttachmentRequest.File.Name}"
            });
            updateId = createResponse.Id;
        }

        var variables = new
        {
            update_id = updateId
        };

        var map = new
        {
            file = "variables.file"
        };

        var stream = await fileManagementClient.DownloadAsync(addAttachmentRequest.File);
        var bytes = await stream.GetByteData();

        var request = new ApiRequest("/file", GraphQlMutations.AddAttachmentToUpdate, variables, map, Creds)
            .AddFile("file", bytes, addAttachmentRequest.File.Name);
        
        await Client.ExecuteWithErrorHandling<DataWrapperDto<AddFileToUpdateResponse>>(request);
        return await GetUpdateAsync(new UpdateIdentifier { ItemId = addAttachmentRequest.ItemId, UpdateId = updateId });
    }
    
    [Action("Edit update", Description = "Edits an update (comment) based on the specified ID")]
    public async Task<UpdateResponse> EditUpdateAsync([ActionParameter] EditUpdateRequest createUpdateRequest)
    {
        var variables = new
        {
            id = createUpdateRequest.UpdateId,
            body = createUpdateRequest.Body
        };
        
        var request = new ApiRequest(GraphQlMutations.EditUpdate, variables, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<EditUpdateResponse>>(request);
        return response.Data.EditUpdate;
    }
    
    [Action("Delete update", Description = "Deletes an update (comment) based on the specified ID")]
    public async Task DeleteUpdateAsync([ActionParameter] UpdateIdentifier createUpdateRequest)
    {
        var variables = new
        {
            id = createUpdateRequest.UpdateId
        };
        
        var request = new ApiRequest(GraphQlMutations.DeleteUpdate, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
}