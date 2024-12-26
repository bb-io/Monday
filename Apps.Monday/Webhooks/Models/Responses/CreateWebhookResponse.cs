using Newtonsoft.Json;

namespace Apps.Monday.Webhooks.Models.Responses;

public class CreateWebhookResponse
{
    [JsonProperty("create_webhook")]
    public WebhookResponse CreateWebhook { get; set; } = new();
}