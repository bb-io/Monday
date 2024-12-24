using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Boards;

public class BoardResponse
{
    [Display("Board ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Board name")]
    public string Name { get; set; } = string.Empty;
    
    [Display("Items count")]
    public double ItemsCount { get; set; }

    public List<GroupResponse> Groups { get; set; } = new();
}