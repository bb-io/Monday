namespace Apps.Monday.Webhooks.Models.Payloads;

public class StatusChangedPayload : Payload
{
    public string ColumnId { get; set; } = string.Empty;

    public string ColumnType { get; set; } = string.Empty;

    public string ColumnTitle { get; set; } = string.Empty;

    public StatusValueContainer? Value { get; set; }

    public StatusValueContainer? PreviousValue { get; set; }
}

public class StatusValueContainer
{
    public StatusLabel? Label { get; set; }
}

public class StatusLabel
{
    public string Text { get; set; } = string.Empty;
}
