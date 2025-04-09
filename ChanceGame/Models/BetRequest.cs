using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ChanceGame.Models;

public class BetRequest
{
    [JsonProperty("points")]
    [Range(1, long.MaxValue, ErrorMessage = "The stake must be a true positive number.")]
    public long Points { get; set; }
    
    [JsonProperty("number")]
    [Range(0, 9, ErrorMessage = "chosen number must be between 0 and 9.")]
    public int Number { get; set; }
}