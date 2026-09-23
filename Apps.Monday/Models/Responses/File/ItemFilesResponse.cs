using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.File;

public class ItemFilesResponse
{
    [JsonProperty("assets")]
    public List<AssetFileResponse> Assets { get; set; } = [];
}