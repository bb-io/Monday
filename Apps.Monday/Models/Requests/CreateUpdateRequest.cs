using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Requests;

public class CreateUpdateRequest : ItemIdentifier
{
    [Display("Body text")]
    public string Body { get; set; } = string.Empty;
}