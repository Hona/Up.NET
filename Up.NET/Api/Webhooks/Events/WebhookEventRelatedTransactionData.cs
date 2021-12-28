namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventRelatedTransactionData
{
    [Obsolete("Always `transactions`")]
    public string Type { get; set; }

    public string Id { get; set; }
}