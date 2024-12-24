using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Items;

public class CreateItemResponse
{
    [Display("Created item")]
    public ItemResponse CreateItem { get; set; } = new();
}