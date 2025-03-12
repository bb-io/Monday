using System;
using Apps.Monday.Models.Responses.Items;

namespace Apps.Monday.Models.Dtos;

public class ChangeColumnValueDto
{
    public ItemResponse ChangeColumnValue { get; set; } = new();
}
