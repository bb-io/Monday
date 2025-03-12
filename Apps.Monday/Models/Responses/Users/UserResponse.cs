using Newtonsoft.Json;

namespace Apps.Monday.Models.Responses.Users;

public class UserResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}
