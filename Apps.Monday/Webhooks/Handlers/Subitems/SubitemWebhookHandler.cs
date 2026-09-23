using Apps.Monday.Helpers;
using Apps.Monday.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Monday.Webhooks.Handlers.Subitems;

// Subitems don't live on the board the user picks.
// Monday creates a hidden "Subitems of <board>" board and every subitem is a normal item on THAT board.
//
// This splits each subscription in two:
// - Monday side: registered on the PARENT board with the subitem event name (create_subitem, change_subitem_column_value).
// Monday rejects webhooks on the hidden board so the parent is the only option.
//
// - Bridge side: the payload contains the HIDDEN board id in event.boardId (the parent appears as event.parentItemBoardId)
// and the bridge routes by board id + event. So the subscription must be created under the hidden board.
//
// The event name splits for the same reason: the payload's "type" is the plain item event (create_pulse / update_column_value),
// identical to what a real item change sends. The board id is the only thing that distinguishes them
public abstract class SubitemWebhookHandler(
    InvocationContext invocationContext,
    BoardIdentifier boardIdentifier) : BaseWebhookHandler(invocationContext, boardIdentifier)
{
    private BoardHelper BoardHelper { get; } = new(invocationContext);
    
    // Bridge subscription goes under the hidden subitems board (see explanation above)
    protected override Task<string> GetBridgeBoardIdAsync() => BoardHelper.GetSubitemsBoardId(boardIdentifier.BoardId);
}