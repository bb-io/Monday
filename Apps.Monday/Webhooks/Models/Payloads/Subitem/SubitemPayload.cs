namespace Apps.Monday.Webhooks.Models.Payloads.Subitem;

public class SubitemPayload : Payload
{
    public string ParentItemId { get; set; } = string.Empty;

    public string ParentItemBoardId { get; set; } = string.Empty;
}