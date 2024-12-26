using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Models.Responses.Updates;
using Apps.Monday.Webhooks.Handlers.Updates;
using Apps.Monday.Webhooks.Models;
using Apps.Monday.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Monday.Webhooks;

[WebhookList]
public class UpdateWebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Webhook("On update created", typeof(UpdateCreatedHandler), 
        Description = "This event is triggered when an update is created")]
    public Task<WebhookResponse<UpdateResponse>> OnUpdateCreated(WebhookRequest request) 
        => HandleWebhookRequest(request);

    [Webhook("On update edited", typeof(UpdateEditedHandler), 
        Description = "This event is triggered when an update is edited")]
    public Task<WebhookResponse<UpdateResponse>> OnUpdateEdited(WebhookRequest request) 
        => HandleWebhookRequest(request);

    private async Task<WebhookResponse<UpdateResponse>> HandleWebhookRequest(WebhookRequest request)
    {
        var body = request.Body.ToString()!;
        var challenge = JsonConvert.DeserializeObject<ChallengeDto>(body);

        if (challenge != null && !string.IsNullOrEmpty(challenge.Challenge))
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(body);
            return new WebhookResponse<UpdateResponse>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                HttpResponseMessage = response
            };
        }

        var itemPayload = JsonConvert.DeserializeObject<EventPayload<CreateUpdatePayload>>(body)!;
        var item = await GetUpdateAsync(itemPayload.Event.PulseId, itemPayload.Event.UpdateId);
        return new WebhookResponse<UpdateResponse>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = item
        };
    }
    
    private async Task<UpdateResponse> GetUpdateAsync(string itemId, string updateId)
    {
        var item = await GetItemAsync(itemId);

        var specificUpdate = item.Updates.FirstOrDefault(x => x.Id == updateId)
                             ?? throw new PluginApplicationException(
                                 $"Unable to find an update with the specified ID ({updateId}) in the item with ID {item}");

        return specificUpdate;
    }
    
    private async Task<ItemResponse> GetItemAsync(string itemId)
    {
        var variables = new { ids = int.Parse(itemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException($"Unable to find an item with the specified ID ({itemId})");
        }

        return response.Data.Items.First();
    }
}