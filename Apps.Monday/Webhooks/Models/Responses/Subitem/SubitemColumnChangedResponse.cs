using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Webhooks.Models.Payloads.Subitem;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Webhooks.Models.Responses.Subitem;

public class SubitemColumnChangedResponse : SubitemWebhookResponse
{
    public SubitemColumnChangedResponse() { }

    public SubitemColumnChangedResponse(ItemResponse subitem, SubitemColumnChangedPayload payload) : base(subitem, payload)
    {
        ColumnId = payload.ColumnId;
        ColumnTitle = payload.ColumnTitle;
        ColumnType = payload.ColumnType;
    }

    [Display("Changed column ID")]
    public string ColumnId { get; set; } = string.Empty;

    [Display("Changed column title")]
    public string ColumnTitle { get; set; } = string.Empty;

    [Display("Changed column type")]
    public string ColumnType { get; set; } = string.Empty;
}