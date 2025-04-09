namespace ChanceGame.Models;
using Newtonsoft.Json;
public class User
{
    [JsonProperty("username")]
    public string Username { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
    
}