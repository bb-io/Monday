using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks.Handlers.Items;

public class ItemCreatedHandler(InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier) : BaseWebhookHandler(invocationContext, boardIdentifier)
{
    protected override string Event => "create_item";
}