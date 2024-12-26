using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks.Handlers;

public abstract class BaseWebhookHandler(
    InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier)
    : AppInvocable(invocationContext), IWebhookEventHandler
{
    protected abstract string Event { get; }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var variables = new
        {
            board_id = boardIdentifier.BoardId,
            url = values["payloadUrl"],
            @event = Event
        };

        var request = new ApiRequest(GraphQlMutations.CreateWebhook, variables, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        // Users can delete the subscription in the UI, so this is ignored
        return Task.CompletedTask;
    }
}