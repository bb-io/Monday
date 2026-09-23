using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks.Handlers.Subitems;

public class SubitemColumnChangedHandler(
    InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier) : SubitemWebhookHandler(invocationContext, boardIdentifier)
{
    protected override string Event => "change_subitem_column_value";
    protected override string BridgeEvent => "change_column_value";
}