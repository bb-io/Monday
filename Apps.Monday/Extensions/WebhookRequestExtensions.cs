using Apps.Monday.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Monday.Extensions;

public static class WebhookRequestExtensions
{
    public static T Deserialize<T>(this WebhookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Body.ToString()))
            throw new PluginApplicationException("Empty payload received");

        var deserialized = JsonConvert.DeserializeObject<EventPayload<T>>(request.Body.ToString()!);
        if (deserialized is null)
            throw new PluginApplicationException($"Failed to deserialize the payload. Raw: {request.Body}");
        
        return deserialized.Event;
    }
}