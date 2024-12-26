using Apps.Monday.Api;
using Apps.Monday.Constants;
using Apps.Monday.Invocables;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.Monday.Webhooks.Handlers;

public abstract class BaseWebhookHandler(
    InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier)
    : AppInvocable(invocationContext), IWebhookEventHandler
{
    private const string AppName = "monday";

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
        var createWebhookResponse =
            await Client.ExecuteWithErrorHandling<DataWrapperDto<CreateWebhookResponse>>(request);

        var bridgeClient =
            new RestClient($"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/storage/{AppName}");

        var encodedPayload = Uri.EscapeDataString(values["payloadUrl"]);
        var bridgePostRequest = new RestRequest($"/payload?{encodedPayload}", Method.Post)
            .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken)
            .AddBody(createWebhookResponse.Data.CreateWebhook.Id);

        await bridgeClient.ExecuteAsync(bridgePostRequest);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var bridgeClient =
            new RestClient($"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/storage/{AppName}");

        var encodedPayload = Uri.EscapeDataString(values["payloadUrl"]);
        var bridgeGetRequest = new RestRequest($"/payload?{encodedPayload}", Method.Get)
            .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);

        var response = await bridgeClient.ExecuteAsync(bridgeGetRequest);
        if (response.IsSuccessStatusCode)
        {
            var webhookId = response.Content!;
            var rawId = webhookId.Trim('"');
            var variables = new
            {
                id = int.Parse(rawId)
            };

            var request = new ApiRequest(GraphQlMutations.DeleteWebhook, variables, Creds);
            await Client.ExecuteWithErrorHandling(request);

            var bridgeDeleteRequest = new RestRequest($"/{encodedPayload}", Method.Delete)
                .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
            await bridgeClient.ExecuteAsync(bridgeDeleteRequest);
        }
        else
        {
            throw new InvalidOperationException(
                $"Couldn't retrieve webhook ID from the bridge. Key: {encodedPayload}, Bridge response: Status code: {response.StatusCode}, Body: {response.Content}");
        }
    }
}