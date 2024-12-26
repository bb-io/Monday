using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.Items;

public class SearchItemsResponse : BaseSearchResponse<ItemResponse>
{
    [Display("Items"), JsonProperty("items")]
    public override List<ItemResponse> Items { get; set; } = new();
}