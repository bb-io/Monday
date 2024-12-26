using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses;

public class BaseSearchResponse<T>
{
    public virtual List<T> Items { get; set; } = new();
    
    [Display("Total count")]
    public double TotalCount { get; set; }
}