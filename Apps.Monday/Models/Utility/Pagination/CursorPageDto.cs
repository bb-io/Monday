namespace Apps.Monday.Models.Utility.Pagination;

public class CursorPageDto<T>
{
    public string? Cursor { get; set; }

    public List<T> Items { get; set; } = new();
}