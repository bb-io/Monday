using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses;

public class GroupResponse
{
    [Display("Board ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Board title")]
    public string Title { get; set; } = string.Empty;
}