using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using TestPlugin.DynamicHandlers;
using TestPlugin.DynamicHandlers.StaticDataHandlers;

namespace Apps.Monday.Webhooks.Models.Responses;

public class ItemIdResponse
{
    [Display("Item ID")]
    public string ItemId { get; set; } = string.Empty;

    [DataSource(typeof(DynamicSimpleHandler))]
    [Display("Text (Dynamic)")]
    public string? InputTextDynamic { get; set; }

    [DataSource(typeof(DynamicItemsSimpleHandler))]
    [Display("Text (Dynamic NH)")]
    public string? InputTextDynamicNewHandler { get; set; }

    [StaticDataSource(typeof(SimpleStaticDataHandler))]
    [Display("Text (Static)")]
    public string? InputTextStatic { get; set; }

    [StaticDataSource(typeof(SimpleStaticItemsDataHandler))]
    [Display("Text (Static NH)")]
    public string? InputTextStaticNewHandler { get; set; }

    [Display("Text")]
    public string? InputText { get; set; }

    [Display("Number")]
    public double? InputNumber { get; set; }

    [Display("Boolean")]
    public bool? InputBoolean { get; set; }

    [Display("Date")]
    public DateTime? InputDate { get; set; }

    [Display("File")]
    public FileReference? InputFile { get; set; }

    [Display("Multiple Texts (Dynamic)")]
    public List<string> OutputDynamicStrings { get; set; }

    [Display("Multiple Texts (Dynamic NH)")]
    public List<string> OutputDynamicStringsNewHandler { get; set; }

    [Display("Multiple Texts (Static)")]
    public List<string> OutputStaticStrings { get; set; }

    [Display("Multiple Texts (Static NH)")]
    public List<string> OutputStaticStringsNewHandler { get; set; }

    [Display("Multiple Texts")]
    public List<string> OutputStrings { get; set; }

    [Display("Multiple Numbers")]
    public List<int> OutputInts { get; set; }

    [Display("Multiple Booleans")]
    public List<bool> OutputBooleans { get; set; }

    [Display("Multiple Dates")]
    public List<DateTime> OutputDateTimes { get; set; }

    [Display("Multiple Files")]
    public List<FileReference> OutputFiles { get; set; }
}