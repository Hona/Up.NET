namespace Up.NET.Api.Attachments;

public class AttachmentAttributes
{
    public DateTime? CreatedAt { get; set; }
    public string FileURL { get; set; }
    public DateTime? FileURLExpiresAt { get; set; }
    public string FileExtension { get; set; }
    public string FileContentType { get; set; }
}
