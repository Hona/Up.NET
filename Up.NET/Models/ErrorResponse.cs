using System.Net;
using System.Text.Json.Serialization;
using Up.NET.Converters;

namespace Up.NET.Models;

public class ErrorResponse
{
    [JsonConverter(typeof(StringToHttpStatusCodeConverter))]
    public HttpStatusCode Status { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public ErrorSource Source { get; set; }
}