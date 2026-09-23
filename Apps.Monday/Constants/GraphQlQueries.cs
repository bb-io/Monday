namespace Apps.Monday.Constants;

public static class GraphQlQueries
{
    private const string ItemFields = @"
        id
        name
        created_at
        updated_at
        email
        url
        relative_link
        updates {
            id
            text_body
            edited_at
            assets {
                id
                name
                file_extension
            }
        }
        assets {
            id
            name
            file_extension
        }
        board {
            id
            name
        }
    ";

    public const string GetBoardWithItemsById = @"
        query($ids: ID!, $limit: Int!) {
            boards(ids: [$ids]) {
                items_page(limit: $limit) {
                    cursor
                    items {
                        " + ItemFields + @"
                    }
                }
            }
        }
    ";

    public const string GetNextItemsPage = @"
        query($cursor: String!, $limit: Int!) {
            next_items_page(cursor: $cursor, limit: $limit) {
                cursor
                items {" + ItemFields + @"}
            }
        }
    ";
    
    public const string GetUserData = "query {users (limit:50) {created_at email account { name id}}}";
    
    public const string GetBoards = @"
        query($limit: Int!, $page: Int!) {
            boards(limit: $limit, page: $page) {
                id
                name
                groups {
                    id
                    title
                }
            }
        }
    ";

    public const string GetBoardById = @"
        query($ids: ID!) {
            boards(ids: [$ids]) {
                id
                name
                items_count
                groups {
                    id
                    title
                }
            }
        }
    ";
    
    public const string GetBoardGroups = @"
        query($ids: ID!) {
            boards(ids: [$ids]) {
                groups {
                    id
                    title
                }
            }
        }
    ";    
    
    public const string GetBoardColumns = @"
        query($ids: ID!) {
            boards(ids: [$ids]) {
                columns {
                    id
                    title
                    type
                }
            }
        }
    ";
    
    public const string GetItemById = @"
        query($ids: ID!) {
            items(ids: [$ids]) {
                id
                name
                created_at
                updated_at      
                email          
                url
                relative_link
                column_values {
                    id
                    text
                    type
                    column {
                        title
                    }
                }
                updates {
                    id
                    text_body
                    edited_at
                    assets {
                        id
                        name
                        file_extension
                    }
                }
                assets {
                    id
                    name
                    file_extension
                }
                board {
                    id,
                    name
                }
            }
        }
    ";
    
    public const string GetItemAssetsById = @"
        query($ids: ID!) {
            items(ids: [$ids]) {
                assets {
                    id
                    name
                    file_extension
                    public_url
                }
            }
        }
    ";

    public const string GetBoardWebhooks = @"
        query($board_id: ID!) {
            webhooks(board_id: $board_id, app_webhooks_only: true) {
                id
                board_id
                event
                config
            }
        }
    ";

    public const string GetSubitemsByItemId = @"
        query($ids: ID!) {
            items(ids: [$ids]) {
                id
                subitems {
                    id
                    name
                    created_at
                    updated_at
                    board {
                        id
                        name
                    }
                    column_values {
                        id
                        text
                        type
                        value
                        column {
                            id
                            title
                        }
                    }
                }
            }
        }
    ";

    public const string GetUsers = @"
        query {
            users {
                id
                name
            }
        }
    ";
    
    public const string GetSubitemsColumnSettings = @"
        query($ids: ID!) {
            boards(ids: [$ids]) {
                columns(types: [subtasks]) {
                    id
                    settings_str
                }
            }
        }
    ";
}
