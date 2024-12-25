using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Identifiers;

public class UpdateIdentifier : ItemIdentifier
{
    [Display("Update ID"), DataSource(typeof(UpdateDataSource))]
    public string UpdateId { get; set; } = string.Empty;
}