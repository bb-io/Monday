using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Requests;

public class CreateItemRequest : BoardIdentifier
{
    [Display("Item name")]
    public string ItemName { get; set; } = string.Empty;

    [Display("Group ID"), DataSource(typeof(GroupDataHandler))]
    public string? GroupId { get; set; }

    [Display("Column IDs"), DataSource(typeof(ColumnDataHandler))]
    public IEnumerable<string>? ColumnIds { get; set; }

    [Display("Column values")]
    public IEnumerable<string>? ColumnValues { get; set; }
}