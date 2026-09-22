using Apps.Monday.Helpers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Webhooks.Handlers.Subitems;

public abstract class SubitemWebhookHandler(
    InvocationContext invocationContext,
    BoardIdentifier boardIdentifier) : BaseWebhookHandler(invocationContext, boardIdentifier)
{
    private BoardHelper BoardHelper { get; } = new(invocationContext);

    protected override Task<string> GetBridgeBoardIdAsync() => BoardHelper.GetSubitemsBoardId(boardIdentifier.BoardId);
}