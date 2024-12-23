using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses;

public class SearchBoardsResponse : BaseSearchResponse<BoardResponse>
{
    [Display("Boards"), JsonProperty("boards")]
    public override List<BoardResponse> Items { get; set; } = new();
}