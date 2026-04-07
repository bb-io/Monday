using Apps.Monday.Models.Responses.Boards;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Monday.Models.Responses.Items;

public class SubitemResponse
{
    [Display("Subitem ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Subitem name")]
    public string Name { get; set; } = string.Empty;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [Display("Parent board")]
    public ParentBoardResponse Board { get; set; } = new();

    [Display("Fields")]
    public List<SubitemFieldResponse> Fields { get; set; } = new();
}

public class SubitemFieldResponse
{
    [Display("Column ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Column title")]
    public string Title { get; set; } = string.Empty;

    [Display("Column type")]
    public string Type { get; set; } = string.Empty;

    [Display("Text value")]
    public string Text { get; set; } = string.Empty;

    [Display("Raw value")]
    public string Value { get; set; } = string.Empty;
}

public class ItemWithSubitemsResponse
{
    public string Id { get; set; } = string.Empty;

    public List<SubitemGraphQlResponse> Subitems { get; set; } = new();
}

public class SubitemGraphQlResponse
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ParentBoardResponse Board { get; set; } = new();

    public List<SubitemColumnValueResponse> ColumnValues { get; set; } = new();
}

public class SubitemColumnValueResponse
{
    public string Id { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public ColumnMetadataResponse Column { get; set; } = new();
}

public class ColumnMetadataResponse
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
