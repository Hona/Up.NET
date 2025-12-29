namespace Up.NET.Api.Attachments;

public class AttachmentRelated
{
    [Obsolete("Always `attachments`")]
    public string Type { get; set; }
    
    public string Id { get; set; }
}
