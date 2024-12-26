using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses;

public class ColumnResponse
{
    [Display("Column ID")]
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}