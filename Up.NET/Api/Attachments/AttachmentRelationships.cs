using Up.NET.Models;

namespace Up.NET.Api.Attachments;

public class AttachmentRelationships
{
    public RelatedData<AttachmentRelatedTransaction> Transaction { get; set; }
}
