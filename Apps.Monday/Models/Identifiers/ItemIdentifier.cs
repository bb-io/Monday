using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Identifiers;

public class ItemIdentifier : BoardIdentifier
{
    [Display("Item ID"), DataSource(typeof(ItemDataHandler))]
    public string ItemId { get; set; } = string.Empty;
}