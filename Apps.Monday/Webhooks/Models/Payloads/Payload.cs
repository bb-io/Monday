namespace Apps.Monday.Webhooks.Models.Payloads;

public class Payload
{
    public string BoardId { get; set; } = string.Empty;
    
    public string PulseId { get; set; } = string.Empty;
    
    public string GroupId { get; set; } = string.Empty;
    
    public string GroupName { get; set; } = string.Empty;
}