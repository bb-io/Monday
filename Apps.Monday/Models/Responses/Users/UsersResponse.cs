using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.Users;

public class UsersResponse
{
    [JsonProperty("users")]
    public IEnumerable<UserResponse> Users { get; set; } = new List<UserResponse>();
}
