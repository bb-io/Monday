namespace Apps.Monday.Webhooks.Models.Payloads;

public class EventPayload<T>
{
    public T Event { get; set; } = default!;
}