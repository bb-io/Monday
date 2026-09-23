namespace Apps.Monday.Models.Utility.Pagination;

public interface ICursorPageSource<T>
{
    CursorPageDto<T>? GetFirstPage();
}