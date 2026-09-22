using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.File;

public class AssetFileResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("file_extension")]
    public string FileExtension { get; set; } = string.Empty;

    [JsonProperty("public_url")]
    public string PublicUrl { get; set; } = string.Empty;
}