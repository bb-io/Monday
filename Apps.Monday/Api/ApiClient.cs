using Apps.Monday.Constants;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Monday.Api;

public class ApiClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    : BlackBirdRestClient(new()
    {
        BaseUrl = new Uri(ApiConstants.BaseUrl + ApiConstants.ApiVersion),
        ThrowOnAnyError = false
    })
{
    public async Task<T1> PaginateAsync<T1, T2>(string query, int limit = 10) where T1 : BaseSearchResponse<T2>, new()
    {
        var page = 1;
        var allItems = new List<T2>();

        while (true)
        {
            var variables = new { limit, page };
            var request = new ApiRequest(query, variables, authenticationCredentialsProviders);
            
            var response = await ExecuteWithErrorHandling<DataWrapperDto<T1>>(request);
            if (response?.Data == null || !response.Data.Items.Any())
            {
                break;
            }

            allItems.AddRange(response.Data.Items);
            page++;
        }

        return new()
        {
            Items = allItems,
            TotalCount = allItems.Count
        };
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        throw new PluginApplicationException(response.Content!);
    }
    
    protected override JsonSerializerSettings JsonSettings => JsonConfig.JsonSettings;
}