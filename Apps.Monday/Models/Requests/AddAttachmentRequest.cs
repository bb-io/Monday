using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Monday.Models.Requests;

public class AddAttachmentRequest : ItemIdentifier
{
    [Display("Update ID"), DataSource(typeof(UpdateDataSource))]
    public string? UpdateId { get; set; }

    [Display("File")]
    public FileReference File { get; set; } = default!;
}