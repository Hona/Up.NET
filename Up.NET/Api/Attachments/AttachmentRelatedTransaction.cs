namespace Up.NET.Api.Attachments;

public class AttachmentRelatedTransaction
{
    [Obsolete("Always `transactions`")]
    public string Type { get; set; }
    
    public string Id { get; set; }
}
