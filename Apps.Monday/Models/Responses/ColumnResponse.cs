using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses;

public class ColumnResponse
{
    [Display("Column ID")]
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    // JSON-encoded string
    [DefinitionIgnore, JsonProperty("settings_str")]
    public string? SettingsStr { get; set; }
}