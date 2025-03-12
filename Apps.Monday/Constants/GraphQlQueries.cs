namespace Apps.Monday.Constants;

public static class GraphQlQueries
{
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

    public const string GetBoardWithItemsById = @"
        query($ids: ID!) {
            boards(ids: [$ids]) {
                items_page {
                    items {
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
                   }
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

    public const string GetUsers = @"
        query {
            users {
                id
                name
            }
        }
    ";
}