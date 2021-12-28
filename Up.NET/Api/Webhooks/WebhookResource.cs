using Up.NET.Models;

namespace Up.NET.Api.Webhooks;

public class WebhookResource
{
    [Obsolete("Always `webhooks`")] public string Type { get; set; }
    public string Id { get; set; }
    public WebhookAttributes Attributes { get; set; }
    public WebhookRelationships Relationships { get; set; }
    public SelfLink Links { get; set; }
}