namespace Up.NET.Api.Webhooks;

public class WebhookAttributes
{
    public string Url { get; set; }
    public string Description { get; set; }
    public string SecretKey { get; set; }
    public DateTime CreatedAt { get; set; }
}