using System;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.DataSourceHandlers;

public class PersonColumnDataHandler(InvocationContext invocationContext, [ActionParameter] BoardIdentifier boardIdentifier) 
    : ColumnDataHandler(invocationContext, boardIdentifier)
{
    protected override List<string>? ColumnTypes => new() { "people" };
}
