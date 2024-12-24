using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Boards;

public class ParentBoardResponse
{
    [Display("Board ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Board name")]
    public string Name { get; set; } = string.Empty;
}