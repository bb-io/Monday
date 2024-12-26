using Apps.Monday.Api;
using Apps.Monday.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Monday.Connections;

public class ConnectionValidator: IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        var credentialsProviders = authenticationCredentialsProviders as AuthenticationCredentialsProvider[] ??
                                   authenticationCredentialsProviders.ToArray();

        var apiClient = new ApiClient(credentialsProviders);
        var request = new ApiRequest(GraphQlQueries.GetUserData, credentialsProviders);

        var response = await apiClient.ExecuteAsync(request, cancellationToken);
        return new()
        {
            IsValid = response.IsSuccessful,
            Message = response.Content
        };
    }
}