using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Identifiers;

public class BoardIdentifier
{
    [Display("Board ID"), DataSource(typeof(BoardDataHandler))]
    public string BoardId { get; set; } = string.Empty;
}