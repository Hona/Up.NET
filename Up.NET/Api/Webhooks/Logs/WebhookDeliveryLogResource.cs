namespace Up.NET.Api.Webhooks.Logs;

public class WebhookDeliveryLogResource
{
    [Obsolete("Always `webhook-delivery-logs`")]
    public string Type { get; set; }

    public string Id { get; set; }
    public WebhookDeliveryLogAttributes Attributes { get; set; }
    public WebhookDeliveryLogRelationships Relationships { get; set; }
}