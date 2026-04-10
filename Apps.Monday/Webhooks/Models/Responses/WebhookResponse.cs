using Newtonsoft.Json;

namespace Apps.Monday.Webhooks.Models.Responses;

public class WebhookResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("board_id")]
    public string BoardId { get; set; } = string.Empty;

    [JsonProperty("event")]
    public string? Event { get; set; }

    [JsonProperty("config")]
    public string? Config { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}
