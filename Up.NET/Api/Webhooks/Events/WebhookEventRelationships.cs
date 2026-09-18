namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventRelationships
{
    public WebhookEventRelatedWebhook Webhook { get; set; }
    public WebhookEventRelatedTransaction Transaction { get; set; }
}
