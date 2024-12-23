namespace Apps.Monday.Constants;

public class GraphQlConstants
{
    public const string GetUserData = @"{
   ""query"": ""query {users (limit:50) {created_at email account { name id}}}"" 
}";
}