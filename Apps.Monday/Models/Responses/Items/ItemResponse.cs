using Apps.Monday.Models.Responses.Boards;
using Apps.Monday.Models.Responses.Updates;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Items;

public class ItemResponse
{
    [Display("Item ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Item name")]
    public string Name { get; set; } = string.Empty;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [Display("Email")]
    public string Email { get; set; } = string.Empty;

    [Display("Item URL")]
    public string Url { get; set; } = string.Empty;

    [Display("Relative link")]
    public string RelativeLink { get; set; } = string.Empty;

    [Display("Parent board")]
    public ParentBoardResponse Board { get; set; } = new();
    
    public List<UpdateResponse> Updates { get; set; } = new();

    public List<AssetResponse> Assets { get; set; } = new();

    [DefinitionIgnore]
    public List<ColumnValueResponse> ColumnValues { get; set; } = new();

    [Display("Status")]
    public string Status => ColumnValues.FirstOrDefault(cv => cv.Id.Equals("status", StringComparison.OrdinalIgnoreCase))?.Text ?? string.Empty;
}
public class ColumnValueResponse
{
    [Display("Column ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Text")]
    public string Text { get; set; } = string.Empty;
}