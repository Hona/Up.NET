namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventRelatedWebhookData
{
    [Obsolete("Always `webhooks`")]
    public string Type { get; set; }

    public string Id { get; set; }
}