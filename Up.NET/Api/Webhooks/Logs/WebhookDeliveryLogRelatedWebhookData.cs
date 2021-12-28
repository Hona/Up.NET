namespace Up.NET.Api.Webhooks.Logs;

public class WebhookDeliveryLogRelatedWebhookData
{
    [Obsolete("Always `webhook-events`")]
    public string Type { get; set; }
    public string Id { get; set; }
}