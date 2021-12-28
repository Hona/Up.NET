namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventResource
{
    [Obsolete("Always `webhook-events`")]
    public string Type { get; set; }
    public string Id { get; set; }
    public WebhookEventAttributes Attributes { get; set; }
    public WebhookEventRelationships Relationships { get; set; }
}