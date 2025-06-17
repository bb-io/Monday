using Apps.Monday.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using TestPlugin.DynamicHandlers;
using TestPlugin.DynamicHandlers.StaticDataHandlers;

namespace Apps.Monday.Models.Identifiers;

public class BoardIdentifier
{
    [Display("Board ID"), DataSource(typeof(BoardDataHandler))]
    public string BoardId { get; set; } = string.Empty;

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