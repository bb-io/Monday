using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Updates;

public class UpdateResponse
{
    [Display("Update ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Text")]
    public string TextBody { get; set; } = string.Empty;

    [Display("Edited at")]
    public DateTime EditedAt { get; set; }

    public List<AssetResponse> Assets { get; set; } = new();
}