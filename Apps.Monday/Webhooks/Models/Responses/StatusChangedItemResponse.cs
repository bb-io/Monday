using Apps.Monday.Models.Responses.Boards;
using Apps.Monday.Models.Responses;
using Apps.Monday.Models.Responses.Items;
using Apps.Monday.Models.Responses.Updates;
using Apps.Monday.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Webhooks.Models.Responses;

public class StatusChangedItemResponse
{
    [Display("Item ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Item name")]
    public string Name { get; set; } = string.Empty;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [Display("Email")]
    public string Email { get; set; } = string.Empty;

    [Display("Item URL")]
    public string Url { get; set; } = string.Empty;

    [Display("Relative link")]
    public string RelativeLink { get; set; } = string.Empty;

    [Display("Changed status")]
    public string Status { get; set; } = string.Empty;

    [Display("Previous status")]
    public string PreviousStatus { get; set; } = string.Empty;

    [Display("Status column ID")]
    public string StatusColumnId { get; set; } = string.Empty;

    [Display("Status column title")]
    public string StatusColumnTitle { get; set; } = string.Empty;

    [Display("Parent board")]
    public ParentBoardResponse Board { get; set; } = new();

    public List<UpdateResponse> Updates { get; set; } = new();

    public List<AssetResponse> Assets { get; set; } = new();

    [DefinitionIgnore]
    public List<ColumnValueResponse> ColumnValues { get; set; } = new();

    public static StatusChangedItemResponse From(ItemResponse item, StatusChangedPayload payload) => new()
    {
        Id = item.Id,
        Name = item.Name,
        CreatedAt = item.CreatedAt,
        UpdatedAt = item.UpdatedAt,
        Email = item.Email,
        Url = item.Url,
        RelativeLink = item.RelativeLink,
        Board = item.Board,
        Updates = item.Updates,
        Assets = item.Assets,
        ColumnValues = item.ColumnValues,
        Status = payload.Value?.Label?.Text ?? string.Empty,
        PreviousStatus = payload.PreviousValue?.Label?.Text ?? string.Empty,
        StatusColumnId = payload.ColumnId,
        StatusColumnTitle = payload.ColumnTitle
    };
}
