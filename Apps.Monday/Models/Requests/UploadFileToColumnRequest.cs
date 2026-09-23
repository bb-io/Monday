using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Monday.Models.Requests;

public class UploadFileToColumnRequest
{
    [Display("File column ID"), DataSource(typeof(FileColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;

    [Display("File")]
    public FileReference File { get; set; } = null!;
}