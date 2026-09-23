namespace Apps.Monday.Webhooks.Models.Payloads.Subitem;

public class SubitemColumnChangedPayload : SubitemPayload
{
    public string ColumnId { get; set; } = string.Empty;

    public string ColumnType { get; set; } = string.Empty;

    public string ColumnTitle { get; set; } = string.Empty;
}