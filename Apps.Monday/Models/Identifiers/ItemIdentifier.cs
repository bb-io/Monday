using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using TestPlugin.DynamicHandlers;
using TestPlugin.DynamicHandlers.StaticDataHandlers;

namespace Apps.Monday.Models.Identifiers;

public class ItemIdentifier : BoardIdentifier
{
    [Display("Item ID"), DataSource(typeof(ItemDataHandler))]
    public string ItemId { get; set; } = string.Empty;
}