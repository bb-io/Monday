using Apps.Monday.Models.Responses.Items;

namespace Apps.Monday.Models.Responses.Boards;

public class BoardWithItemsResponse
{
    public ItemsPageResponse ItemsPage { get; set; } = new();
}