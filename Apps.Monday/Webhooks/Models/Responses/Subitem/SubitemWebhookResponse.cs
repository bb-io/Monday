using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Webhooks.Models.Payloads.Subitem;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Webhooks.Models.Responses.Subitem;

public class SubitemWebhookResponse
{
    public SubitemWebhookResponse() { }
    
    public SubitemWebhookResponse(ItemResponse subitem, SubitemPayload payload)
    {
        Subitem = subitem;
        ParentItemId = payload.ParentItemId;
        ParentItemBoardId = payload.ParentItemBoardId;
    }

    [Display("Subitem")]
    public ItemResponse Subitem { get; set; }

    [Display("Parent item ID")]
    public string ParentItemId { get; set; }

    [Display("Parent board ID")]
    public string ParentItemBoardId { get; set; }
}