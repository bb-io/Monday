namespace Apps.Monday.Webhooks.Models.Payloads;

public class DeleteItemPayload
{
    public string BoardId { get; set; } = string.Empty;

    public string? ItemId { get; set; }
    
    public string GroupId { get; set; } = string.Empty;
    
    public string GroupName { get; set; } = string.Empty;
}