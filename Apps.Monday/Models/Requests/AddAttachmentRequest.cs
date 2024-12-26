using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Monday.Models.Requests;

public class AddAttachmentRequest : UpdateIdentifier
{
    [Display("File")]
    public FileReference File { get; set; } = default!;
}