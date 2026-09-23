using Apps.Monday.Constants;
using Apps.Monday.Models.Dtos;
using Apps.Monday.Models.Responses;
using Apps.Monday.Models.Utility.Pagination;
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

    public async Task<List<T>?> PaginateByCursor<TFirstPage, T>(
        string firstPageQuery,
        Dictionary<string, object> firstPageVariables,
        string nextPageQuery,
        int limit = 500)
        where TFirstPage : ICursorPageSource<T>
    {
        var variables = new Dictionary<string, object>(firstPageVariables) { ["limit"] = limit };

        var firstRequest = new ApiRequest(firstPageQuery, variables, authenticationCredentialsProviders);
        var firstResponse = await ExecuteWithErrorHandling<DataWrapperDto<TFirstPage>>(firstRequest);

        var page = firstResponse?.Data?.GetFirstPage();
        if (page == null)
            return null;

        var allItems = new List<T>(page.Items);

        while (!string.IsNullOrEmpty(page.Cursor))
        {
            var nextRequest = new ApiRequest(
                nextPageQuery, 
                new { cursor = page.Cursor, limit },
                authenticationCredentialsProviders);
            var nextResponse = await ExecuteWithErrorHandling<DataWrapperDto<NextPageDto<T>>>(nextRequest);

            page = nextResponse?.Data?.Page ?? throw new PluginApplicationException("Unable to retrieve the next page of results");
            allItems.AddRange(page.Items);
        }

        return allItems;
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        throw new PluginApplicationException(response.Content!);
    }
    
    protected override JsonSerializerSettings JsonSettings => JsonConfig.JsonSettings;
}