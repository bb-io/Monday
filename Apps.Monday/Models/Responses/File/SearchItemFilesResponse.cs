using Apps.Monday.Models.Responses.Items;
using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.File;

public class SearchItemFilesResponse
{
    [JsonProperty("items")]
    public List<ItemFilesResponse> Items { get; set; } = new();
}