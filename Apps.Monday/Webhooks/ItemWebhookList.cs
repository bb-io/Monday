using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Webhooks.Handlers.Items;
using Apps.Monday.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Monday.Webhooks;

[WebhookList]
public class ItemWebhookList
{
    [Webhook("On item created", typeof(ItemCreatedHandler), 
        Description = "This event is triggered when item is created.")]
    public Task<WebhookResponse<ItemResponse>> OnItemCreated(WebhookRequest request,
        [WebhookParameter] BoardIdentifier boardIdentifier) => HandleWebhookRequest(request);
    
    private Task<WebhookResponse<ItemResponse>> HandleWebhookRequest(WebhookRequest request)
    {
        var body = request.Body.ToString()!;
        var challenge = JsonConvert.DeserializeObject<ChallengeDto>(body);

        if (challenge != null && !string.IsNullOrEmpty(challenge.Challenge))
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(body);
            return Task.Run(() => new WebhookResponse<ItemResponse>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                HttpResponseMessage = response
            });
        }
        
        return Task.Run(() => new WebhookResponse<ItemResponse>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = new()
            {
                Id = body // TEMP
            }
        });
    }
}