using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Models.Utility.Pagination;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Boards;

public class BoardItemsResponse : ICursorPageSource<ItemResponse>
{
    [Display("Boards")]
    public List<BoardWithItemsResponse> Boards { get; set; } = new();

    public CursorPageDto<ItemResponse>? GetFirstPage() => Boards.FirstOrDefault()?.ItemsPage;
}