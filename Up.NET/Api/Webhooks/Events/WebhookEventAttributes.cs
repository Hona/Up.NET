namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventAttributes
{
    public WebhookEventType EventType { get; set; }
    public DateTime CreatedAt { get; set; }
}