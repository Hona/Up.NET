using Up.NET.Models;

namespace Up.NET.Api.Webhooks.Events;

public class WebhookEventRelatedTransaction
{
    public WebhookEventRelatedTransactionData Data { get; set; }
    public RelatedLink Links { get; set; }
}