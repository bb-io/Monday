using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Webhooks.Bridge;
using Apps.Monday.Webhooks.Models.Responses;
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

    protected virtual string? GetWebhookConfig() => null;

    protected virtual bool MatchesExistingWebhook(WebhookResponse webhook) =>
        MatchesBridgeTarget(webhook) && MatchesDefaultConfig(webhook.Config);

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var bridge = CreateBridgeService(authenticationCredentialsProvider);
        bridge.Subscribe(Event, boardIdentifier.BoardId, values["payloadUrl"]);

        var existingWebhooks = await GetBoardWebhooksAsync();
        if (existingWebhooks.Any(MatchesExistingWebhook))
        {
            return;
        }

        var variables = new
        {
            board_id = boardIdentifier.BoardId,
            url = GetBridgeWebhookUrl(),
            @event = Event,
            config = GetWebhookConfig()
        };

        var request = new ApiRequest(GraphQlMutations.CreateWebhook, variables, Creds);
        await Client.ExecuteWithErrorHandling<DataWrapperDto<CreateWebhookResponse>>(request);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var bridge = CreateBridgeService(authenticationCredentialsProvider);
        bridge.Unsubscribe(Event, boardIdentifier.BoardId, values["payloadUrl"]);

        if (bridge.IsAnySubscriberExist(Event, boardIdentifier.BoardId))
        {
            return;
        }

        var matchingWebhooks = (await GetBoardWebhooksAsync())
            .Where(MatchesExistingWebhook)
            .ToList();

        foreach (var webhook in matchingWebhooks)
        {
            var variables = new
            {
                id = int.Parse(webhook.Id)
            };

            var request = new ApiRequest(GraphQlMutations.DeleteWebhook, variables, Creds);
            await Client.ExecuteWithErrorHandling(request);
        }
    }

    protected bool MatchesBridgeTarget(WebhookResponse webhook) =>
    string.Equals(webhook.Event, Event, StringComparison.OrdinalIgnoreCase);

    private BridgeService CreateBridgeService(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider) =>
        new(authenticationCredentialsProvider, GetBridgeWebhookUrl());

    private string GetBridgeWebhookUrl() =>
        $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/webhooks/monday";

    private async Task<List<WebhookResponse>> GetBoardWebhooksAsync()
    {
        var variables = new
        {
            board_id = boardIdentifier.BoardId
        };

        var request = new ApiRequest(GraphQlQueries.GetBoardWebhooks, variables, Creds);
        var response = await Client.ExecuteWithErrorHandling<DataWrapperDto<ListWebhooksResponse>>(request);

        return response.Data.Webhooks;
    }

    private bool MatchesDefaultConfig(string? actualConfig)
    {
        var expectedConfig = GetWebhookConfig();
        if (string.IsNullOrWhiteSpace(expectedConfig))
        {
            return string.IsNullOrWhiteSpace(actualConfig) || actualConfig == "{}";
        }

        return NormalizeConfig(actualConfig) == NormalizeConfig(expectedConfig);
    }

    private static string NormalizeConfig(string? config) =>
        string.IsNullOrWhiteSpace(config)
            ? string.Empty
            : new string(config.Where(x => !char.IsWhiteSpace(x)).ToArray());
}
