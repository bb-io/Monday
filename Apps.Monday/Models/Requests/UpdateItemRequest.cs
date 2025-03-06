using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Requests
{
    public class UpdateItemRequest : ItemIdentifier
    {
        [Display("Column", Description ="Column to update")]
        public string ColumnId { get; set; } = string.Empty;

        [Display("Value", Description = "Value to update specified column")]
        public string Value { get; set; } = string.Empty;
    }
}
