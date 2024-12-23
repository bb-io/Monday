using Apps.Monday.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.Monday.Api;

public class ApiClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    : BlackBirdRestClient(new()
    {
        BaseUrl = new Uri(ApiConstants.BaseUrl + ApiConstants.ApiVersion),
        ThrowOnAnyError = false
    })
{
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        throw new Exception(response.Content!);
    }
}