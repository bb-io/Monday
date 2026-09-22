using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks.Handlers.Subitems;

public class SubitemCreatedHandler(
    InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier) 
    : BaseWebhookHandler(invocationContext, boardIdentifier)
{
    protected override string Event => "create_subitem";
}