using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Webhooks.Models.Responses;

public class ItemIdResponse
{
    [Display("Item ID")]
    public string ItemId { get; set; } = string.Empty;
}