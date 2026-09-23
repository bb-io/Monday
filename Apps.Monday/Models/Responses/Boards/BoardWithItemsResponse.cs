using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Models.Utility.Pagination;

namespace Apps.Monday.Models.Responses.Boards;

public class BoardWithItemsResponse
{
    public CursorPageDto<ItemResponse> ItemsPage { get; set; } = new();
}