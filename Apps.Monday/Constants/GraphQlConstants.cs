namespace Apps.Monday.Constants;

public class GraphQlConstants
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
}