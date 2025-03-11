using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Requests;

public class UpdateItemRequest : ItemIdentifier
{
    [Display("Column ID", Description = "Column to update"), DataSource(typeof(ColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;

    [Display("Value", Description = "Value to update specified column")]
    public string Value { get; set; } = string.Empty;
}
