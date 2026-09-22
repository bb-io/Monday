using Apps.Monday.Extensions;
using Apps.Monday.Helpers;
using Apps.Monday.Invocables;
using Apps.Monday.Webhooks.Handlers.Subitems;
using Apps.Monday.Webhooks.Models.Payloads.Subitem;
using Apps.Monday.Webhooks.Models.Responses.Subitem;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks;

[WebhookList("Subitems")]
public class SubitemWebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    private readonly ItemHelper _itemHelper = new(invocationContext);
    
    [Webhook("On subitem created", typeof(SubitemCreatedHandler), Description = "This event is triggered when a subitem is created")]
    public async Task<WebhookResponse<SubitemWebhookResponse>> OnSubitemCreated(WebhookRequest request)
    {
        var payload = request.Deserialize<SubitemPayload>();
        var subitem = await _itemHelper.GetItem(payload.PulseId);

        return new WebhookResponse<SubitemWebhookResponse>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = new SubitemWebhookResponse(subitem, payload)
        };
    }
}