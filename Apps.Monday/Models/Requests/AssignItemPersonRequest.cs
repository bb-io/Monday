using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Requests;

public class AssignItemPersonRequest : ItemIdentifier
{
    [Display("Column ID"), DataSource(typeof(ColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;

    [Display("Person ID"), DataSource(typeof(UserDataHandler))]
    public string PersonId { get; set; } = string.Empty;
}
