using Newtonsoft.Json;

namespace Apps.Monday.Webhooks.Bridge.Models;

public class BridgeGetResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("value")]
    public string Value { get; set; } = string.Empty;
}
