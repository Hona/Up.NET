using Up.NET.Models;

namespace Up.NET.Api.Webhooks.Events
{
    public class WebhookEventRelatedWebhook
    {
        public WebhookEventRelatedWebhookData Data { get; set; }
        public RelatedLink Links { get; set; }
    }
}