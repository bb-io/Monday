using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Monday.Webhooks.Handlers.Updates;

public class UpdateEditedHandler(InvocationContext invocationContext,
    [WebhookParameter] BoardIdentifier boardIdentifier) : BaseWebhookHandler(invocationContext, boardIdentifier)
{
    protected override string Event => "edit_update";
}