using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses;

public class AssetResponse
{
    [Display("Asset ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Asset name")]
    public string Name { get; set; } = string.Empty;

    [Display("File extension")]
    public string FileExtension { get; set; } = string.Empty;
}