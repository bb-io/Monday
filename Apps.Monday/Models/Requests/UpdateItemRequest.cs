using Apps.Monday.DataSourceHandlers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using TestPlugin.DynamicHandlers;
using TestPlugin.DynamicHandlers.StaticDataHandlers;

namespace Apps.Monday.Models.Requests;

public class UpdateItemRequest : ItemIdentifier
{
    [Display("Column ID", Description = "Column to update"), DataSource(typeof(ColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;

    [Display("Value", Description = "Value to update specified column")]
    public string Value { get; set; } = string.Empty;

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

    [DataSource(typeof(DynamicSimpleHandler))]
    [Display("Multiple Texts (Dynamic)", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 2.")]
    public List<string>? InputStringsDynamic { get; set; }

    [DataSource(typeof(DynamicItemsSimpleHandler))]
    [Display("Multiple Texts (Dynamic NH)", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 2.")]
    public List<string>? InputStringsDynamicNewHandler { get; set; }

    [StaticDataSource(typeof(SimpleStaticDataHandler))]
    [Display("Multiple Texts (Static)", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 2.")]
    public List<string>? InputStringsStatic { get; set; }

    [StaticDataSource(typeof(SimpleStaticItemsDataHandler))]
    [Display("Multiple Texts (Static NH)", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 2.")]
    public List<string>? InputStringsStaticNewHandler { get; set; }

    [Display("Multiple Texts", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 2.")]
    public List<string>? InputStrings { get; set; }

    [Display("Multiple Numbers", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 3.")]
    public List<int>? InputInts { get; set; }

    [Display("Multiple Booleans", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 5.")]
    public List<bool>? InputBooleans { get; set; }

    [Display("Multiple Dates", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 4.")]
    public List<DateTime>? InputDateTimes { get; set; }

    [Display("Multiple Files", Description = "This is test description of the item for testing purposes. This text should look fine. Test tooltip 6.")]
    public List<FileReference>? InputFiles { get; set; }
}
