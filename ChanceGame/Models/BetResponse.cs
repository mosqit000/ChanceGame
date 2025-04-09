using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChanceGame.Models;


public class BetResponse
{
    [JsonProperty("account")]
    public long? Account { get; set; }   
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("points")]
    public string Points { get; set; }
}