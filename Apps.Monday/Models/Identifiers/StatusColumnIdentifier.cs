using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Identifiers;

public class StatusColumnIdentifier
{
    [Display("Board ID"), DataSource(typeof(BoardDataHandler))]
    public string BoardId { get; set; } = string.Empty;

    [Display("Status field"), DataSource(typeof(StatusColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;
}
