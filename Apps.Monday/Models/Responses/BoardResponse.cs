using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses;

public class BoardResponse
{
    [Display("Board ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Board name")]
    public string Name { get; set; } = string.Empty;
}