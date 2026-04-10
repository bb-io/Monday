using Newtonsoft.Json;

namespace Apps.Monday.Webhooks.Models.Responses;

public class ListWebhooksResponse
{
    [JsonProperty("webhooks")]
    public List<WebhookResponse> Webhooks { get; set; } = [];
}
