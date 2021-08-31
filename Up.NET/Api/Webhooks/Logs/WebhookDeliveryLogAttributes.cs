using System;

namespace Up.NET.Api.Webhooks.Logs
{
    public class WebhookDeliveryLogAttributes
    {
        public WebhookDeliveryLogRequest Request { get; set; }
        public WebhookDeliveryLogResponse Response { get; set; }
        public WebhookDeliveryStatus DeliveryStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}