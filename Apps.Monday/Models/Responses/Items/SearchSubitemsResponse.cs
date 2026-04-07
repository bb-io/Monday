using Apps.Monday.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.Items;

public class SearchSubitemsResponse : BaseSearchResponse<SubitemResponse>
{
    [Display("Subitems")]
    public override List<SubitemResponse> Items { get; set; } = new();
}

public class SubitemsQueryResponse : BaseSearchResponse<ItemWithSubitemsResponse>
{
    [JsonProperty("items")]
    public override List<ItemWithSubitemsResponse> Items { get; set; } = new();
}
