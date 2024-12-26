using Apps.Monday.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Monday.Api;

public class ApiRequest : BlackBirdRestRequest
{
    public ApiRequest(string query, IEnumerable<AuthenticationCredentialsProvider> creds) : base(String.Empty, Method.Post, creds)
    {
        this.AddBody(new { query });
    }
    
    public ApiRequest(string query, object variables, IEnumerable<AuthenticationCredentialsProvider> creds)
        : base(string.Empty, Method.Post, creds)
    {
        this.AddBody(new { query, variables });
    }
    
    public ApiRequest(string endpoint, string query, object variables, object map, IEnumerable<AuthenticationCredentialsProvider> creds)
        : base(endpoint, Method.Post, creds)
    {
        this.AddParameter("query", query);
        this.AddParameter("variables", JsonConvert.SerializeObject(variables));
        this.AddParameter("map", JsonConvert.SerializeObject(map));
    }
    
    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var apiToken = creds.Get(CredsNames.AccessToken).Value;
        this.AddHeader("Authorization", apiToken);
    }
}