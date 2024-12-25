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

namespace Apps.Monday.Actions;

[ActionList]
public class UpdateActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get update", Description = "Get update based on Item ID")] 
    public async Task<UpdateResponse> GetUpdateAsync([ActionParameter] UpdateIdentifier updateIdentifier)
    {
        var itemActions = new ItemActions(InvocationContext);
        var item = await itemActions.GetItemAsync(updateIdentifier);

        var specificUpdate = item.Updates.FirstOrDefault(x => x.Id == updateIdentifier.UpdateId)
                             ?? throw new PluginApplicationException(
                                 $"Couldn't find update based on specified ID ({updateIdentifier.UpdateId}) in item with {updateIdentifier.ItemId} ID");

        return specificUpdate;
    }
    
    [Action("Add update", Description = "Create update (comment) in specific item based on ID")]
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
    
    [Action("Edit update", Description = "Edit an update (comment) based on specified ID")]
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
    
    [Action("Delete update", Description = "Delete an update (comment) based on specified ID")]
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