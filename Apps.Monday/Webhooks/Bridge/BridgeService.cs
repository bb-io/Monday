using Apps.Monday.Constants;
using Apps.Monday.Webhooks.Bridge.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Monday.Webhooks.Bridge;

public class BridgeService
{
    private readonly string _bridgeServiceUrl;

    public BridgeService(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, string bridgeServiceUrl)
    {
        _bridgeServiceUrl = bridgeServiceUrl;
    }

    public void Subscribe(string @event, string boardId, string url)
    {
        var client = new RestClient(_bridgeServiceUrl);
        var request = new RestRequest($"/{boardId}/{@event}", Method.Post);
        request.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        request.AddBody(url);

        var response = client.Execute(request);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Failed to subscribe to event {@event} for board {boardId}. Body: {response.Content}");
        }
    }

    public void Unsubscribe(string @event, string boardId, string url)
    {
        var client = new RestClient(_bridgeServiceUrl);
        var requestGet = new RestRequest($"/{boardId}/{@event}", Method.Get);
        requestGet.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        var webhooks = client.Get<List<BridgeGetResponse>>(requestGet) ?? [];

        var webhook = webhooks.FirstOrDefault(w => w.Value == url);
        if (webhook == null)
        {
            return;
        }

        var requestDelete = new RestRequest($"/{boardId}/{@event}/{webhook.Id}", Method.Delete);
        requestDelete.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        client.Delete(requestDelete);
    }

    public bool IsAnySubscriberExist(string @event, string boardId)
    {
        var client = new RestClient(_bridgeServiceUrl);
        var request = new RestRequest($"/{boardId}/{@event}", Method.Get);
        request.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        var response = client.Get<List<BridgeGetResponse>>(request);

        return response?.Any() ?? false;
    }
}
