namespace Apps.Monday.Webhooks.Models;

public class WebhookDto
{
    public string Id { get; set; } = string.Empty;

    public string Event { get; set; } = string.Empty;

    public string BoardId { get; set; } = string.Empty;

    public string Config { get; set; } = string.Empty;
}