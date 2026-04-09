using Apps.Monday.Models.Identifiers;
using Apps.Monday.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Monday.Webhooks.Handlers.Items;

public class StatusChangedHandler(InvocationContext invocationContext, [WebhookParameter] BoardIdentifier boardIdentifier,
    [WebhookParameter] StatusColumnIdentifier statusColumnIdentifier) : BaseWebhookHandler(
        invocationContext, boardIdentifier)
{
    protected override string Event => "change_status_column_value";

    protected override string GetWebhookConfig()
    {
        var config = new
        {
            columnId = new
            {
                boardId = boardIdentifier.BoardId,
                columnId = statusColumnIdentifier.ColumnId,
                columnType = "color",
                isSubitemColumn = false
            },
            columnValue = new Dictionary<string, bool>
            {
                ["$any$"] = true
            }
        };

        return JsonConvert.SerializeObject(config);
    }

    protected override bool MatchesExistingWebhook(WebhookResponse webhook) =>
        MatchesBridgeTarget(webhook)
        && !string.IsNullOrWhiteSpace(webhook.Config)
        && webhook.Config.Contains(boardIdentifier.BoardId, StringComparison.OrdinalIgnoreCase)
        && webhook.Config.Contains(statusColumnIdentifier.ColumnId, StringComparison.OrdinalIgnoreCase)
        && webhook.Config.Contains("color", StringComparison.OrdinalIgnoreCase);
}
