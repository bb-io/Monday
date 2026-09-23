using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses;

public class SettingsStrResponse
{
    [JsonProperty("boardIds")]
    public List<string> BoardIds { get; set; } = [];
}