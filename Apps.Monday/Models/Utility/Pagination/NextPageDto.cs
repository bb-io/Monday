using Newtonsoft.Json;

namespace Apps.Monday.Models.Utility.Pagination;

public class NextPageDto<T>
{
    [JsonProperty("next_items_page")]
    public CursorPageDto<T> Page { get; set; } = new();
}