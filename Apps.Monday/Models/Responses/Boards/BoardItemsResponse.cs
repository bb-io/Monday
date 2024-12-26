using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Boards;

public class BoardItemsResponse
{
    [Display("Boards")]
    public List<BoardWithItemsResponse> Boards { get; set; } = new();
}