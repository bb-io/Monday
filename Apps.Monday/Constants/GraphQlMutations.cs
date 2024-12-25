namespace Apps.Monday.Constants;

public static class GraphQlMutations
{
    public const string CreateItem = @"mutation($board_id: ID!, $item_name: String!, $group_id: String, $column_values: JSON) {
        create_item (board_id: $board_id, item_name: $item_name, group_id: $group_id, column_values: $column_values) {
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
                id,
                 name
            }
        }
    }";
    
    public const string CreateUpdate = @"mutation($item_id: ID!, $body: String!) {
        create_update (item_id: $item_id, body: $body) {
            id
            text_body
            edited_at
            assets {
                id
                name
                file_extension
            }
        }
    }";
    
    public const string EditUpdate = @"mutation($id: ID!, $body: String!) {
        edit_update (id: $id, body: $body) {
            id
            text_body
            edited_at
            assets {
                id
                name
                file_extension
            }
        }
    }";

    public const string AddAttachmentToUpdate = @"mutation($update_id: ID!, $file: File!) { 
        add_file_to_update (update_id: $update_id, file: $file) {
            id
            name
            file_extension
        }
    }";
    
    public const string DeleteUpdate = @"mutation($id: ID!) {
        delete_update (id: $id) {
            id
        }
    }";
    
    public const string DeleteItem = @"mutation($item_id: ID!) {
        delete_item (item_id: $item_id) {
            id
        }
    }"; 
    
    public const string ArchiveItem = @"mutation($item_id: ID!) {
        archive_item (item_id: $item_id) {
            id
        }
    }";
    
    public const string CreateWebhook = @"mutation($board_id: ID!, $url: String!, $event: WebhookEventType!) {
        create_webhook (board_id: $board_id, url: $url, event: $event) {
            id 
            board_id
        }
    }";
}