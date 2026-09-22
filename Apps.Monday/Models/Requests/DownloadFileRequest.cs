using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Monday.Models.Requests;

public class DownloadFileRequest
{
    [Display("File asset ID"), DataSource(typeof(ItemFileAssetDataHandler))]
    public string FileId { get; set; } = string.Empty;
}