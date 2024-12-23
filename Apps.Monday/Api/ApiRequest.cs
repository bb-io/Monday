using Apps.Monday.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.Monday.Api;

public class ApiRequest : BlackBirdRestRequest
{
    public ApiRequest(string resource, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) : base(resource, method, creds)
    {
        
    }
    
    public ApiRequest(IEnumerable<AuthenticationCredentialsProvider> creds) : base(String.Empty, Method.Post, creds)
    {
        
    }
    
    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var apiToken = creds.Get(CredsNames.AccessToken).Value;
        this.AddHeader("Authorization", apiToken);
    }
}