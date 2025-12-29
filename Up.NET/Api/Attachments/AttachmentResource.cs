using Up.NET.Models;

namespace Up.NET.Api.Attachments;

public class AttachmentResource
{
    [Obsolete("Always `attachments`")]
    public string Type { get; set; }
    
    public string Id { get; set; }
    public AttachmentAttributes Attributes { get; set; }
    public AttachmentRelationships Relationships { get; set; }
    public SelfLink Links { get; set; }
}
