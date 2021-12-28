using System.Text.Json.Serialization;

namespace Up.NET.Models;

public class PaginatedLinks
{
    [JsonPropertyName("prev")]
    public string Previous { get; set; }
    public string Next { get; set; }
        
    public bool HasNext => Next != null;
    public bool HasPrevious => Previous != null;
}