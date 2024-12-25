using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Webhooks.Handlers.Items;
using Apps.Monday.Webhooks.Models;
using Apps.Monday.Webhooks.Models.Payloads;
using Apps.Monday.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Monday.Webhooks;

[WebhookList]
public class ItemWebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Webhook("On item created", typeof(ItemCreatedHandler), 
        Description = "This event is triggered when item is created.")]
    public Task<WebhookResponse<ItemResponse>> OnItemCreated(WebhookRequest request) 
        => HandleWebhookRequest(request);

    [Webhook("On item archived", typeof(ItemArchivedHandler),
        Description = "This event is triggered when an item is archived.")]
    public Task<WebhookResponse<ItemIdResponse>> OnItemArchived(WebhookRequest request)
        => HandleArchivedOrDeletedRequest(request);

    [Webhook("On item deleted", typeof(ItemDeletedHandler),
        Description = "This event is triggered when an item is deleted.")]
    public Task<WebhookResponse<ItemIdResponse>> OnItemDeleted(WebhookRequest request)
        => HandleArchivedOrDeletedRequest(request);
    
    private async Task<WebhookResponse<ItemResponse>> HandleWebhookRequest(WebhookRequest request)
    {
        var body = request.Body.ToString()!;
        var challenge = JsonConvert.DeserializeObject<ChallengeDto>(body);

        if (challenge != null && !string.IsNullOrEmpty(challenge.Challenge))
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(body);
            return new WebhookResponse<ItemResponse>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                HttpResponseMessage = response
            };
        }

        var itemPayload = JsonConvert.DeserializeObject<EventPayload<ItemPayload>>(body)!;
        var item = await GetItem(itemPayload.Event.PulseId);
        return new WebhookResponse<ItemResponse>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = item
        };
    }
    
    private Task<WebhookResponse<ItemIdResponse>> HandleArchivedOrDeletedRequest(WebhookRequest request)
    {
        var body = request.Body.ToString()!;
        var challenge = JsonConvert.DeserializeObject<ChallengeDto>(body);

        if (challenge != null && !string.IsNullOrEmpty(challenge.Challenge))
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(body)
            };

            return Task.FromResult(new WebhookResponse<ItemIdResponse>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                HttpResponseMessage = response
            });
        }

        var itemPayload = JsonConvert.DeserializeObject<EventPayload<ItemPayload>>(body)!;
        return Task.FromResult(new WebhookResponse<ItemIdResponse>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = new()
            {
                ItemId = itemPayload.Event.PulseId
            }
        });
    }

    private async Task<ItemResponse> GetItem(string itemId)
    {
        var variables = new { ids = int.Parse(itemId) };
        var request = new ApiRequest(GraphQlQueries.GetItemById, variables, Creds);

        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<SearchItemsResponse>>(request);
        if (response?.Data == null || !response.Data.Items.Any())
        {
            throw new PluginApplicationException($"Couldn't find board by specified ID ({itemId})");
        }

        return response.Data.Items.First();
    }
}